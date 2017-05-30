﻿using System;
using System.Collections.Generic;
using TexasHoldem.Logic.Game;
using TexasHoldem.Logic.Game_Control;
using TexasHoldem.Logic.Notifications_And_Logs;
using TexasHoldem.Logic.Replay;
using TexasHoldem.Logic.Users;
using TexasHoldemShared.CommMessages;

namespace TexasHoldem.Logic.GameControl
{
    public class GameCenter
    {
        private List<Log> logs;

        public int leagueGap { get; set; }
        private List<IGame> games;

        private static int roomIdCounter = 1;
        private SystemControl _systemControl ;
        private LogControl logControl;
        private ReplayManager replayManager;

        private static readonly object padlock = new object();

        public GameCenter(SystemControl sys, LogControl log, ReplayManager replay)
        {
            replayManager = replay;
            _systemControl = sys;
            logControl = log;
            this.logs = new List<Log>();
            this.games = new List<IGame>();
        }

        public bool DoAction(IUser user, CommunicationMessage.ActionType action, int amount, int roomId)
        {
            IGame gm = GetRoomById(roomId);

            return gm.DoAction(user, action, amount, true);
        }

        public List<Player> getPlayersInRoom(int roomId)
        {
            IGame gm = GetRoomById(roomId);
            return gm.GetPlayersInRoom();
        }

        public List<Spectetor> getSpectatorsInRoom(int roomId)
        {
            IGame gm = GetRoomById(roomId);
            return gm.GetSpectetorInRoom();
        }

        //minBet is the BB
        public bool CreateNewRoomWithRoomId(int roomId, IUser user, int startingChip, bool canSpectate, GameMode gameModeChosen,
            int minPlayersInRoom, int maxPlayersInRoom, int enterPayingMoney, int minBet)
        {
            lock (padlock)
            {
                if (user == null || startingChip < 0)
                {
                    return false;
                }
                if (minPlayersInRoom <= 0)
                {
                    ErrorLog log = new ErrorLog("Error while trying to create room, mim amount of player is invalid - less or equal to zero");
                    logControl.AddErrorLog(log);
                    return false;
                }
                if (minBet <= 0)
                {
                    ErrorLog log = new ErrorLog("Error while trying to create room, min bet is invalid - less or equal to zero");
                    logControl.AddErrorLog(log);
                    return false;
                }
                if (maxPlayersInRoom <= 0)
                {
                    ErrorLog log = new ErrorLog("Error while trying to create room, Max amount of player is invalid - less or equal to zero");
                    logControl.AddErrorLog(log);
                    return false;
                }
                if (minPlayersInRoom > maxPlayersInRoom)
                {
                    ErrorLog log = new ErrorLog("Error while trying to create room, invalid input - min player in room is bigger than max player in room");
                    logControl.AddErrorLog(log);
                    return false;
                }
                if (enterPayingMoney < 0)
                {
                    ErrorLog log = new ErrorLog("Error while trying to create room, invalid input - the entering money of the player is a negative number");
                    logControl.AddErrorLog(log);
                    return false;
                }
                List<Player> players = new List<Player>();
                if (enterPayingMoney > 0)
                {
                    int newMoney = user.Money() - enterPayingMoney;
                    if (!user.ReduceMoneyIfPossible(enterPayingMoney))
                    {
                        ErrorLog log = new ErrorLog("not enough money to pay the fee for user id: " + user.Id());
                        logControl.AddErrorLog(log);
                        return false;
                    }
                }
                Player player = new Player(user, startingChip , roomId);
                players.Add(player);
                Decorator decorator = CreateDecorator(minBet, startingChip, canSpectate, minPlayersInRoom, maxPlayersInRoom, enterPayingMoney, gameModeChosen, user.GetLeague());
                ServerToClientSender sender = new ServerToClientSender(this, _systemControl, logControl, replayManager);
                GameRoom room = new GameRoom(players, roomId, decorator, this, logControl, replayManager, sender);
                return AddRoom(room);
            }
        }

        private Decorator CreateDecorator(int minBet, int startingChip, bool canSpectate, int minPlayersInRoom, int maxPlayersInRoom, int enterPayingMoney, GameMode gameModeChosen, LeagueName league)
        {
            Decorator mid = new MiddleGameDecorator(gameModeChosen, minBet, minBet / 2);
            Decorator before = new BeforeGameDecorator(minBet, startingChip, canSpectate, minPlayersInRoom, maxPlayersInRoom, enterPayingMoney, league);
            before.SetNextDecorator(mid);
            return before;
        }

        //return the next room Id
        public int GetNextIdRoom()
        {
            lock (padlock)
            {
                int toReturn = System.Threading.Interlocked.Increment(ref roomIdCounter);
                return toReturn;
            }
        }

        private int CurrRoomId()
        {
            return roomIdCounter;
        }
        //Add new room the games list
        public bool AddRoom(GameRoom roomToAdd)
        {
            lock (padlock)
            {
                bool toReturn = false;
                if (roomToAdd == null)
                {
                    return toReturn;
                }
                try
                {
                    this.games.Add(roomToAdd);
                    toReturn = true;
                }
                catch (Exception e)
                {
                    ErrorLog log = new ErrorLog("Error while trying to add new room to game center");
                    logControl.AddErrorLog(log);
                    toReturn = false;
                }
                return toReturn;
            }
        }

        //remove room form games list - remove the room remove the game from active game list and spectetor in user
        public bool RemoveRoom(int roomId)
        {
            lock (padlock)
            {
                bool toReturn = false;
                IGame toRemove = GetRoomById(roomId);
                try
                {
                    games.Remove(toRemove);
                    toReturn = true;
                }
                catch (Exception e)
                {
                    ErrorLog log = new ErrorLog("Error while trying to remove game room");
                    logControl.AddErrorLog(log);
                    toReturn = false;
                }
                return toReturn;
            }
        }

        public List<IGame> GetAllActiveGamesAUserCanJoin(IUser user)
        {
            List<IGame> toReturn = new List<IGame>();
            lock (padlock)
            {
                int userMoney = user.Money();
                int userPoints = user.Points();
                bool isUnKnow = user.IsUnKnow();
                foreach (IGame room in games)
                {
                    if (room.CanJoin(user))
                    {
                        toReturn.Add(room);
                    }
                }
            }
            return toReturn;
        }

        //return room by room if - suncronized due to for
        //return null if room Id smaller than 0 or not found
        public IGame GetRoomById(int roomId)
        {
            lock (padlock)
            {
                IGame toReturn = null;
                if (!IsValidInputNotSmallerZero(roomId))
                {
                    return toReturn;
                }
                foreach (IGame room in games)
                {
                    if (room.Id == roomId)
                    {
                        toReturn = room;
                        return toReturn;
                    }
                }
                return toReturn;
            }
        }

        //return true if there is a room with this Id
        public bool IsRoomExist(int roomId)
        {
            lock (padlock)
            {
                bool toReturn = false;
                if (!IsValidInputNotSmallerZero(roomId))
                {
                    return toReturn;
                }
                IGame room = GetRoomById(roomId);
                List<IGame> all = GetAllGames();
                toReturn = all.Contains(room);
                return toReturn;
            }
        }

        //return all games in the system 0 active and non active
        public List<IGame> GetGames()
        {
            return games;
        }

        //get all active games - syncronized
        public List<IGame> GetAllActiveGame()
        {
            lock (padlock)
            {
                List<IGame> toReturn = new List<IGame>();
                foreach (IGame room in games)
                {
                    if (room.IsGameActive())
                    {

                        toReturn.Add(room);
                    }
                }
                return toReturn;
            }
        }

        public List<IGame> GetAllSpectetorGame()
        {
            lock (padlock)
            {
                List<IGame> toReturn = new List<IGame>();
                IGame toAdd = null;
                foreach (IGame room in games)
                {
                    if (room.IsSpectatable())
                    {
                        toAdd = room;
                        toReturn.Add(toAdd);
                    }
                }
                return toReturn;
            }
        }

        //return list of games with pot size
        public List<IGame> GetAllGamesByPotSize(int potSize)
        {
            lock (padlock)
            {
                List<IGame> toReturn = new List<IGame>();
                try
                {
                    if (!IsValidInputNotSmallerZero(potSize))
                    {
                        toReturn = null;
                        return toReturn;
                    }
                    foreach (IGame room in games)
                    {
                        if (room.IsPotSizeEqual(potSize))
                        {
                            toReturn.Add(room);
                        }
                    }
                }
                catch (Exception e)
                {
                    toReturn = null;
                }
                return toReturn;
            }
        }

        //return list of games with game mode:
        //limit / no - limit / pot limit
        public List<IGame> GetGamesByGameMode(GameMode gm)
        {
            lock (padlock)
            {
                List<IGame> toReturn = new List<IGame>();
                foreach (IGame room in games)
                {
                    if (room.IsGameModeEqual(gm))
                    {
                        toReturn.Add(room);
                    }

                }
                return toReturn;
            }
        }

        //return list of games by buy in policy
        public List<IGame> GetGamesByBuyInPolicy(int buyIn)
        {
            lock (padlock)
            {
                List<IGame> toReturn = new List<IGame>();
                try
                {
                    if (!IsValidInputNotSmallerZero(buyIn))
                    {
                        toReturn = null;
                        return toReturn;
                    }
                    foreach (IGame room in games)
                    {
                        if (room.IsGameBuyInPolicyEqual(buyIn))
                        {
                            toReturn.Add(room);
                        }

                    }
                }
                catch (Exception e)
                {
                    toReturn = null;
                }

                return toReturn;
            }
        }

        //return list of games by min player in room
        public List<IGame> GetGamesByMinPlayer(int min)
        {
            lock (padlock)
            {
                List<IGame> toReturn = new List<IGame>();
                try
                {
                    if (!IsValidInputNotSmallerZero(min))
                    {
                        toReturn = null;
                        return toReturn;
                    }
                    foreach (IGame room in games)
                    {
                        if (room.IsGameMinPlayerEqual(min))
                        {
                            toReturn.Add(room);
                        }

                    }
                }
                catch (Exception e)
                {
                    toReturn = null;
                }

                return toReturn;
            }
        }

        //return list of games by max player in room
        public List<IGame> GetGamesByMaxPlayer(int max)
        {
            lock (padlock)
            {
                List<IGame> toReturn = new List<IGame>();
                try
                {
                    if (!IsValidInputNotSmallerEqualZero(max))
                    {
                        toReturn = null;
                        return toReturn;
                    }
                    foreach (IGame room in games)
                    {
                        if (room.IsGameMaxPlayerEqual(max))
                        {
                            toReturn.Add(room);
                        }

                    }
                }
                catch (Exception e)
                {
                    toReturn = null;
                }

                return toReturn;
            }
        }

        //return list of games by min bet in room
        //syncronized - due to for
        public List<IGame> GetGamesByMinBet(int minBet)
        {
            lock (padlock)
            {
                List<IGame> toReturn = new List<IGame>();
                try
                {

                    if (!IsValidInputNotSmallerZero(minBet))
                    {
                        toReturn = null;
                        return toReturn;
                    }
                    foreach (IGame room in games)
                    {
                        if (room.IsGameMinBetEqual(minBet))
                        {
                            toReturn.Add(room);
                        }

                    }
                }
                catch (Exception e)
                {
                    toReturn = null;
                }

                return toReturn;
            }
        }

        //return list of games by starting chip policy
        //return null if startingChup <=0
        //syncronized - due to for
        public List<IGame> GetGamesByStartingChip(int startingChip)
        {
            lock (padlock)
            {
                List<IGame> toReturn = new List<IGame>();
                if (!IsValidInputNotSmallerZero(startingChip))
                {
                    toReturn = null;
                    return toReturn;
                }
                try
                {

                    foreach (IGame room in games)
                    {
                        if (room.IsGameStartingChipEqual(startingChip))
                        {
                            toReturn.Add(room);
                        }

                    }

                }
                catch (Exception e)
                {
                    toReturn = null;
                }
                return toReturn;
            }
        }

        //chaeck if game is spectetable

        public bool IsGameCanSpectete(int roomId)
        {
            bool toReturn = false;
            IGame room = GetRoomById(roomId);
            if (room.IsSpectatable())
            {
                toReturn = true;
            }
            return toReturn;
        }

        //check if game is active game
        public bool IsGameActive(int roomId)
        {
            bool toReturn = false;
            IGame room = GetRoomById(roomId);
            if (room.IsGameActive())
            {
                toReturn = true;
            }
            return toReturn;
        }

        public List<IGame> GetAllGames()
        {
            lock (padlock)
            {
                return games;
            }
        }

        private bool IsValidInputNotSmallerEqualZero(int toCheck)
        {
            return toCheck > 0;
        }

        private bool IsValidInputNotSmallerZero(int toCheck)
        {
            return toCheck >= 0;
        }
        
        //seand notification to user
        public bool SendNotification(User reciver, Notification toSend)
        {
            lock (padlock)
            {
                bool toReturn = false;
                reciver.SendNotification(toSend);
                return toReturn;
            }
        }

        public List<IGame> Games
        {
            get
            {

                return games;
            }

            set
            {
                lock (padlock)
                {
                    games = value;
                }
            }
        }

        public bool CanSendPlayerBrodcast(IUser user, int roomId)
        {
            lock (padlock)
            {
                IGame game = GetRoomById(roomId);
                if (game == null)
                {
                    return false;
                }
                return game.IsPlayerInRoom(user);
            }
        }

        public bool CanSendSpectetorBrodcast(IUser user, int roomId)
        {
            lock (padlock)
            {
                IGame game = GetRoomById(roomId);
                if (game == null)
                {
                    return false;
                }
                return game.IsSpectetorInRoom(user);
            }
        }

        public bool CanSendPlayerWhisper(IUser sender, IUser reciver, int roomId)
        {
            lock (padlock)
            {
                IGame game = GetRoomById(roomId);
                if (game == null)
                {
                    return false;
                }
                bool isSenderPlayer = game.IsPlayerInRoom(sender);
                bool isReceiverActive = reciver.IsLogin();
                bool isReciverSectetorOrPlayer = game.IsPlayerInRoom(reciver) || game.IsSpectetorInRoom(reciver);
                return isSenderPlayer && isReciverSectetorOrPlayer && isReceiverActive;
            }
        }

        public bool CanSendSpectetorWhisper(IUser sender, IUser reciver, int roomId)
        {
            lock (padlock)
            {
                IGame game = GetRoomById(roomId);
                if (game == null)
                {
                    return false;
                }
                bool isSenderSpectetor = game.IsSpectetorInRoom(sender);
                bool isReceiverActive = reciver.IsLogin();
                bool isReciverSpector = game.IsSpectetorInRoom(reciver);
                return isSenderSpectetor && isReciverSpector && isReceiverActive;
            }
        }
    }


}
