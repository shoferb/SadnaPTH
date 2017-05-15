﻿using Client.GuiScreen;
using Client.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemShared.CommMessages;
using TexasHoldemShared.CommMessages.ClientToServer;
using TexasHoldemShared.CommMessages.ServerToClient;

namespace Client.Logic
{
    public class ClientLogic
    {
        private int _userId;
        private ClientEventHandler _eventHandler;
        private CommunicationHandler _handler;
        public List<Tuple<CommunicationMessage, bool, bool,ResponeCommMessage>> messagesSentObserver =  new List<Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>>(); //first bool = is response received, second bool = is succeeded
        private readonly Object listLock;
        private List<GameScreen> _games;

        public ClientUser user { get; set; }
        //chanfajf
        public ClientLogic()
        {
            _games = new List<GameScreen>();
            listLock = new Object();
            //todo - find server name
            user = null;
        }
        //TODO Add specs
        public void GameUpdateReceived(GameDataCommMessage msg)
        {
            bool isNewGame = true;
            foreach (GameScreen game in _games)
            {
                if (game.RoomId == msg.RoomId)
                {
                    isNewGame = false;
                    game.UpdateGame(msg);
                }
            }
            if (isNewGame == false)
            {
                GameScreen newGame = new GameScreen(this);
                newGame.UpdateGame(msg);
                newGame.Show();
            }
        }
        public bool SpectateRoom(int roomId)
        {
            ActionCommMessage toSend = new ActionCommMessage(_userId, CommunicationMessage.ActionType.Spectate, -1, roomId);
            Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> messageToList = new Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>(toSend, false, false, new ResponeCommMessage(_userId));
            messagesSentObserver.Add(messageToList);
            _eventHandler.SendNewEvent(toSend);
            while ((messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item2 == false)
            {
                var t = Task.Run(async delegate { await Task.Delay(1000); });
                t.Wait();
            }
            bool toRet = (messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item3;
            return toRet;

        }
        public bool SetUserId(int newId)
        {
            _userId = newId;
            this._eventHandler.SetNewUserId(newId);
            this._handler.SetUserId(newId);
            return true;
        }
        //needed to be call after create new ClientEventHandler and a new client logic
        public void Init(ClientEventHandler eventHandler, CommunicationHandler handler)
        {
            _eventHandler = eventHandler;
            _handler = handler;
        }

        public void CloseSystem()
        {
            _eventHandler.Close();
            _handler.Close();
        }
        public bool EditDetails(TexasHoldemShared.CommMessages.ClientToServer.EditCommMessage.EditField field, string value)
        {

            EditCommMessage toSend = new EditCommMessage(_userId, field, value);
            Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> messageToList = new Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>(toSend, false, false, new ResponeCommMessage(_userId));
            messagesSentObserver.Add(messageToList);
            _eventHandler.SendNewEvent(toSend);
            while ((messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item2 == false)
            {
                var t = Task.Run(async delegate { await Task.Delay(1000); });
                t.Wait();
            }
            bool toRet = (messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item3;
            messagesSentObserver.Remove(messageToList);
            return toRet;
        }
        public bool JoinTheGame(int roomId)
        {
            ActionCommMessage toSend = new ActionCommMessage(_userId, TexasHoldemShared.CommMessages.CommunicationMessage.ActionType.Join, -1, roomId);
            Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> messageToList = new Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>(toSend, false, false, new ResponeCommMessage(_userId));
            messagesSentObserver.Add(messageToList);
            _eventHandler.SendNewEvent(toSend);
            while ((messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item2 == false)
            {
                var t = Task.Run(async delegate { await Task.Delay(1000); });
                t.Wait();
            }
            bool toRet = (messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item3;
            messagesSentObserver.Remove(messageToList);
            return toRet;
        }

        public int CreateNewRoom(GameMode mode, int minBet, int chipPol, int buyInPol, bool canSpec, int minPlayers, int maxPlayers)
        {//should ret int as the roomNumber
            CreatrNewRoomMessage toSend = new CreatrNewRoomMessage(_userId, mode, minBet, chipPol, buyInPol, canSpec, minPlayers, maxPlayers);
            Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> messageToList = new Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>(toSend, false, false, new ResponeCommMessage(_userId));
            messagesSentObserver.Add(messageToList);
            _eventHandler.SendNewEvent(toSend);
            while ((messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item2 == false)
            {
                var t = Task.Run(async delegate { await Task.Delay(1000); });
                t.Wait();
            }
            bool isSuccessful = (messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item3;
            int newRoomId = 0;//change to room id from response msg
            if(isSuccessful)
            {
                //get room number from response
            }
            else
            {
                newRoomId = -1;
            }
            messagesSentObserver.Remove(messageToList);
            return newRoomId;
        }

        public bool LeaveTheGame(int roomId)
        {
            ActionCommMessage toSend = new ActionCommMessage(_userId, TexasHoldemShared.CommMessages.CommunicationMessage.ActionType.Leave, -1, roomId);
            Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> messageToList = new Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>(toSend, false, false, new ResponeCommMessage(_userId));
            messagesSentObserver.Add(messageToList);
            _eventHandler.SendNewEvent(toSend);
            while ((messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item2 == false)
            {
                var t = Task.Run(async delegate { await Task.Delay(1000); });
                t.Wait();
            }
            bool toRet = (messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item3;
            messagesSentObserver.Remove(messageToList);
            return toRet;

        }

        public bool SendChatMsg(int _roomId, string _ReciverUsername, string _msgToSend, CommunicationMessage.ActionType _chatType)
        {
            ChatCommMessage toSend = new ChatCommMessage(this._userId, _roomId,  _ReciverUsername,  _msgToSend, _chatType);
            Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> messageToList = new Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>(toSend, false, false, new ResponeCommMessage(_userId)); messagesSentObserver.Add(messageToList);
            _eventHandler.SendNewEvent(toSend);
            while ((messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item2 == false)
            {
                var t = Task.Run(async delegate { await Task.Delay(1000); });
                t.Wait();
            }
            bool toRet = (messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item3;
            messagesSentObserver.Remove(messageToList);
            return toRet;
        }


        public bool StartTheGame(int roomId)
        {
            ActionCommMessage toSend = new ActionCommMessage(_userId, TexasHoldemShared.CommMessages.CommunicationMessage.ActionType.StartGame, -1, roomId);
            Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> messageToList = new Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>(toSend, false, false, new ResponeCommMessage(_userId)); messagesSentObserver.Add(messageToList);
            _eventHandler.SendNewEvent(toSend);
            while ((messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item2 == false)
            {
                var t = Task.Run(async delegate { await Task.Delay(1000); });
                t.Wait();
            }
            bool toRet = (messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item3;
            messagesSentObserver.Remove(messageToList);
            return toRet;
        }
       

        public bool Login(string userName, string password)//check with oded
        {
            LoginCommMessage toSend = new LoginCommMessage(_userId, true, userName, password);
            Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> messageToList = new Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>(toSend, false, false, new ResponeCommMessage(_userId)); messagesSentObserver.Add(messageToList);
            _eventHandler.SendNewEvent(toSend);
            while ((messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item2 == false)
            {
                var t = Task.Run(async delegate { await Task.Delay(1000); });
                t.Wait();
            }
            bool toRet = (messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item3;
            
            if (toRet)
            {
                LoginResponeCommMessage rmsg = (LoginResponeCommMessage)(messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item4;
                ClientUser cuser = new ClientUser(rmsg.UserId, rmsg.name, rmsg.username,
                    rmsg.password, rmsg.avatar, rmsg.money, rmsg.email, rmsg.leauge);
                this.user = cuser;
                this._userId = user.id;
            }
            messagesSentObserver.Remove(messageToList);
            return toRet;

        }
        public bool Logout(string userName, string password)
        {
            LoginCommMessage toSend = new LoginCommMessage(_userId, false, userName, password);
            Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> messageToList = new Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>(toSend, false, false, new ResponeCommMessage(_userId)); messagesSentObserver.Add(messageToList);
            _eventHandler.SendNewEvent(toSend);
            while ((messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item2 == false)
            {
                var t = Task.Run(async delegate { await Task.Delay(1000); });
                t.Wait();
            }
            bool toRet = (messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item3;
            messagesSentObserver.Remove(messageToList);
            return toRet;

        }

        public List<ClientGame> SearchGame(int userId, SearchCommMessage.SearchType _searchType, string _searchByString, int _searchByInt, GameMode _searchByGameMode)
        {
            List<ClientGame> toReturn = new List<ClientGame>();
            SearchCommMessage toSend = new SearchCommMessage(userId,  _searchType, _searchByString, _searchByInt,_searchByGameMode);
            Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> messageToList = new Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>(toSend, false, false, new ResponeCommMessage(_userId)); messagesSentObserver.Add(messageToList);
            _eventHandler.SendNewEvent(toSend);
            while ((messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item2 == false)
            {
                var t = Task.Run(async delegate { await Task.Delay(1000); });
                t.Wait();
            }
            bool toRet = (messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item3;

            if (toRet)
            {
                
                SearchResponseCommMessage rmsg = (SearchResponseCommMessage)(messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item4;
                toReturn = rmsg.games;
            }
            messagesSentObserver.Remove(messageToList);
            return toReturn;

        }

        public bool Register(int id,string name, string memberName, string password, int money, string email)
        {
            RegisterCommMessage toSend = new RegisterCommMessage(id, name, memberName, password, money, email);
            Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> messageToList = new Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>(toSend, false, false, new ResponeCommMessage(_userId));
            messagesSentObserver.Add(messageToList);
            _eventHandler.SendNewEvent(toSend);
            while ((messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item2 == false)
            {
                var t = Task.Run(async delegate { await Task.Delay(1000); });
                t.Wait();
            }
            bool toRet = (messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item3;
          
            if (toRet)
            {
                RegisterResponeCommMessage rmsg = (RegisterResponeCommMessage)(messagesSentObserver.Find(x => x.Item1.Equals(toSend))).Item4;
                ClientUser cuser = new ClientUser(rmsg.UserId, rmsg.name, rmsg.username,
                    rmsg.password, rmsg.avatar, rmsg.money, rmsg.email, rmsg.leauge);
                this.user = cuser;
                this._userId = user.id;
            }
            messagesSentObserver.Remove(messageToList);
            return toRet;
        }


        public void gotMsg(ChatResponceCommMessage msg)
        {
            foreach(GameScreen game in _games)
            {
                if(game.RoomId==msg.roomId)
                {
                    game.AddChatMsg(msg);
                }
            }
        }

       
        public bool NotifyChosenMove(TexasHoldemShared.CommMessages.CommunicationMessage.ActionType move, int amount, int roomId)
        {
          
            
                if (move.Equals(TexasHoldemShared.CommMessages.CommunicationMessage.ActionType.Fold))
                {
                    amount = -1;//amount isnt relevant
                    ActionCommMessage response = new ActionCommMessage(_userId, move, amount, roomId);
                    Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> messageToList = new Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>(response, false, false, new ResponeCommMessage(_userId));
                    messagesSentObserver.Add(messageToList);
                    _eventHandler.SendNewEvent(response);
                    while ((messagesSentObserver.Find(x => x.Item1.Equals(response))).Item2 == false)
                    {
                        var t = Task.Run(async delegate { await Task.Delay(1000); });
                        t.Wait();
                    }
                    bool toRet = (messagesSentObserver.Find(x => x.Item1.Equals(response))).Item3;
                    messagesSentObserver.Remove(messageToList);
                    return toRet;
                }
                else if ((move.Equals(TexasHoldemShared.CommMessages.CommunicationMessage.ActionType.Bet)) && (amount >= 0))
                {
                    ActionCommMessage response = new ActionCommMessage(_userId, move, amount, roomId);
                    Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> messageToList = new Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>(response, false, false, new ResponeCommMessage(_userId));
                    messagesSentObserver.Add(messageToList);
                    _eventHandler.SendNewEvent(response);
                    while ((messagesSentObserver.Find(x => x.Item1.Equals(response))).Item2 == false)
                    {
                        var t = Task.Run(async delegate { await Task.Delay(1000); });
                        t.Wait();
                    }
                    bool toRet = (messagesSentObserver.Find(x => x.Item1.Equals(response))).Item3;
                    messagesSentObserver.Remove(messageToList);
                    return toRet;
                }
                else
                {
                    return false;
                }
            

           
        }

        public void NotifyResponseReceived(ResponeCommMessage msg)
        {
            Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> toEdit = messagesSentObserver.Find(x => x.Item1.Equals(msg.OriginalMsg));
            messagesSentObserver.Remove(toEdit);
            Tuple<CommunicationMessage, bool, bool, ResponeCommMessage> toAdd =
                new Tuple<CommunicationMessage, bool, bool, ResponeCommMessage>(toEdit.Item1, true, msg.Success, msg);
            messagesSentObserver.Add(toAdd);

        }
    }
}
