﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using TexasHoldem.Logic.Game;
using TexasHoldem.Logic.Notifications_And_Logs;
using TexasHoldem.Logic.Replay;
using TexasHoldem.Logic.Users;

namespace TexasHoldem.Logic.Game_Control
{
    public class GameCenter
    {
        private List<League> leagueTable;
        private List<Log> logs;
        private User higherRank;
        private int leagueGap;
        private List<ConcreteGameRoom> games; //all games 
        private static int roomIdCounter = 0;
        private ReplayManager _replayManager;

        public GameCenter()
        {
            this.leagueTable = new List<League>();
            //add first league function
            this.logs = new List<Log>();
            this.games = new List<ConcreteGameRoom>();
            _replayManager = new ReplayManager();
        }

        //return thr next room Id
        public int GetNextIdRoom()
        {
            int toReturn = System.Threading.Interlocked.Increment(ref roomIdCounter);
            return toReturn;
        }

        //create new game room
        public bool CreateNewRoom(int userId,int smallBlind,int playerMoney)
        {
            bool toReturn = false;
            if (playerMoney < smallBlind)
            {
                return toReturn;
            }
            int nextId = GetNextIdRoom();
            List<Player> players = new List<Player>();
            SystemControl sc = new SystemControl();
            User user = sc.GetUserWithId(userId);
            
            Player player = new Player(smallBlind, 0, user.Id, user.Name, user.MemberName, user.Password, user.Points,
                user.Money, user.Email, nextId);
            ConcreteGameRoom room = new ConcreteGameRoom(players,smallBlind,nextId, _replayManager);
            toReturn = AddRoom(room);
            return toReturn;
        }

      /* public ConcreteGameRoom GetRoomById(int roomId)
        {
            throw 
        }*/

        public bool RemoveRoom(int gameID)
        {
            bool toReturn = false;

            return toReturn;
        }

        public bool AddRoom(ConcreteGameRoom roomToAdd)
        {
            bool toReturn = false;
            try
            {
                this.games.Add(roomToAdd);
                toReturn = true;
            }
            catch (Exception e)
            {
                toReturn = false;
            }
            return toReturn;
        }

        public bool LeagueChange(int leagugap)
        {
            bool toReturn = false;
            int higherRank = HigherRank.Points;
            leagueTable = null;
            int currpoint = 0;
            int i = 1;
            int to = 0;
            String leaugeName;
            while (currpoint < higherRank)
            {
                leaugeName = "" + i;
                to = currpoint + leagueGap;
                League toAdd = new League(leaugeName, currpoint, to);
                i++;
                currpoint = to;
            }
            return toReturn;
        }

        public string UserLeageInfo(User user)
        {
            string toReturn = "";
            int userRank = user.Points;
            int i = 1;
            int min = 0;
            int max = leagueGap;
            bool flag = ((userRank > min) && (userRank < max));
            while (!flag)
            {
                if ((userRank > min) && (userRank < max))
                {
                    flag = true;
                    toReturn = "" + i;
                }
                else
                {
                    min = max + 1;
                    max = min + leagueGap;
                }
            }
            return toReturn;
        }
        

        public bool SendNotification(User reciver, Notification toSend)
        {
            bool toReturn = false;
            reciver.SendNotification(toSend);
            return toReturn;
        }

        //getters setters
        public List<League> LeagueTable
        {
            get
            {
                return leagueTable;
            }

            set
            {
                leagueTable = value;
            }
        }
        public List<Log> Logs
        {
            get
            {
                return logs;
            }

            set
            {
                logs = value;
            }
        }

        public int LeagueGap
        {
            get
            {
                return LeagueGap;
            }

            set
            {
                LeagueGap = value;
            }
        }



        public User HigherRank
        {
            get
            {
                return higherRank;
            }

            set
            {
                higherRank = value;
            }
        }
    }
}
