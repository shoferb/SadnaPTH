﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TexasHoldem.Database.DataControlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Users;
using TexasHoldem.DatabaseProxy;
using TexasHoldem.Logic.Game;
using TexasHoldem.Logic.GameControl;
using TexasHoldem.Logic.Game_Control;
using TexasHoldem.Logic.Replay;
using TexasHoldem.communication.Impl;
using TexasHoldem.Logic;
using TexasHoldemShared.CommMessages;
using System.Xml.Linq;

namespace TexasHoldem.Database.DataControlers.Tests
{
    [TestClass()]
    public class GameDataControlerTests
    {
        private GameDataProxy proxy;
        private UserDataProxy _userDataProxy;
        private IUser user1, user2, user3;
        private List<Player> players;
        private Player player1;
        private int roomID;
        private GameRoom gameRoom;
        private bool useCommunication;
        private static LogControl logControl = new LogControl();
        private static SystemControl sysControl = new SystemControl(logControl);
        private static ReplayManager replayManager = new ReplayManager();
        private static SessionIdHandler ses = new SessionIdHandler();
        private static GameCenter gameCenter = new GameCenter(sysControl, logControl, replayManager, ses);
        private GameDataControler controller;

        [TestInitialize()]
        public void Initialize()
        {
            controller = new GameDataControler();
            proxy = new GameDataProxy(gameCenter);
            _userDataProxy = new UserDataProxy();
            user1 = new User(1, "test1", "mo", "1234", 0, 5000, "test1@gmail.com");
            user2 = new User(2, "test2", "no", "1234", 0, 5000, "test2@gmail.com");
            user3 = new User(3, "test3", "3test", "1234", 0, 5000, "test3@mailnator.com");
            _userDataProxy.AddNewUser(user1);
            _userDataProxy.AddNewUser(user2);
            _userDataProxy.AddNewUser(user3);
            useCommunication = false;
            roomID = 9999;
            players = new List<Player>();
            player1 = new Player(user1, 1000, roomID);
            player1.RoundChipBet = 22;
            players.Add(player1);
            Decorator deco = SetDecoratoresNoLimitWithSpectatores();
            gameRoom = new GameRoom(players, roomID, deco, gameCenter, logControl, replayManager, ses);
        }

        private void RegisterUser(int userId)
        {
            IUser toAdd = new User(userId, "orelie", "orelie" + userId, "123456789", 0, 15000, "orelie@post.bgu.ac.il");
            _userDataProxy.AddNewUser(toAdd);
        }
        private GameRoom CreateRoomWithId(int gameNum, int roomId, int userId1, int userId2, int userId3)
        {

            RegisterUser(userId1);
            RegisterUser(userId2);
            RegisterUser(userId3);
            useCommunication = false;

            List<Player> toAddPlayers = new List<Player>();
            IUser user = _userDataProxy.GetUserById(userId1);
            Decorator deco = SetDecoratoresNoLimitWithSpectatores();
            player1.RoundChipBet = 22;
            players.Add(player1);
            player1 = new Player(user, 1000, roomId);
            GameRoom gm = new GameRoom(toAddPlayers, roomId, deco, gameCenter, logControl, replayManager, ses);
            gm.GameNumber = gameNum;
            return gm;
        }
        private Decorator SetDecoratoresNoLimitWithSpectatores()
        {
            Decorator mid = new MiddleGameDecorator(GameMode.NoLimit, 10, 5);
            Decorator before = new BeforeGameDecorator(10, 1000, true, 2, 4, 20, LeagueName.A);
            before.SetNextDecorator(mid);
            return before;
        }


        public void Cleanup(int gameNum, int roomId, int userId1, int userId2, int userId3)
        {
            _userDataProxy.DeleteUserById(userId1);
            _userDataProxy.DeleteUserById(userId2);
            _userDataProxy.DeleteUserById(userId3);
            replayManager.DeleteGameReplay(roomID, 0);
            replayManager.DeleteGameReplay(roomID, 1);
            proxy.DeleteGameRoomPref(roomId);
            proxy.DeleteGameRoom(roomId, gameNum);
        }

        [TestMethod()]
        public void InsertGameRoomTest()
        {
            GameRoomXML gamexml = new GameRoomXML(gameRoom);
            Database.LinqToSql.GameRoom toIns = new Database.LinqToSql.GameRoom();
            toIns.GameId = gameRoom.GetGameNum();
            toIns.isActive = gameRoom.IsGameActive();
            toIns.RoomId = gameRoom.Id;
            toIns.GameXML = proxy.GameRoomToXElement(gamexml);
            toIns.Replay = gameRoom.GetGameReplay();
            bool ans = controller.InsertGameRoom(toIns);
            IGame g = proxy.GetGameRoombyId(gameRoom.Id);
            Assert.IsTrue(ans);
            Assert.IsNotNull(g);
            proxy.DeleteGameRoomPref(gameRoom.Id);
            proxy.DeleteGameRoom(gameRoom.Id, gameRoom.GetGameNum());
        }

        [TestMethod()]
        public void UpdateGameRoomPotSizeTest()
        {
            int roomId = new Random().Next();
            int gameId = new Random().Next();
            int userId1 = 1;
            int userId2 = 2;
            int userId3 = 3;
            GameRoom toAdd = CreateRoomWithId(gameId, roomId, userId1, userId2, userId3);
            proxy.InsertNewGameRoom(toAdd);
            bool ans = controller.UpdateGameRoomPotSize(777, roomId);
            Assert.IsTrue(ans);
            Cleanup(gameId, roomId, userId1, userId2, userId3);
        }

        [TestMethod()]
        public void getAllGamesTest()
        {
            int roomId = new Random().Next();
            int gameId = new Random().Next();
            int userId1 = new Random().Next();
            int userId2 = new Random().Next();
            int userId3 = new Random().Next();
            int room2Id = new Random().Next();
            int game2Id = new Random().Next();
            int user2Id1 = new Random().Next();
            int user2Id2 = new Random().Next();
            int user2Id3 = new Random().Next();
            GameRoom toAdd = CreateRoomWithId(gameId, roomId, userId1, userId2, userId3);
            GameRoom toAdd2 = CreateRoomWithId(game2Id, room2Id, user2Id1, user2Id2, user2Id3);
            proxy.InsertNewGameRoom(toAdd);
            proxy.InsertNewGameRoom(toAdd2);
            proxy.UpdateGameRoom(toAdd);
            List<LinqToSql.GameRoom> g = controller.getAllGames();
            bool g1 = false;
            bool g2 = false;
            foreach (var game in g)
            {
                if (game.RoomId == roomId)
                {
                    g1 = true;
                }
                if (game.RoomId == room2Id)
                {
                    g2 = true;
                }
            }
            Assert.AreEqual(g1, true);
            Assert.AreEqual(g2, true);
            Cleanup(gameId, roomId, userId1, userId2, userId3);
            Cleanup(game2Id, room2Id, user2Id1, user2Id2, userId3);
        }

        [TestMethod()]
        public void GetAllActiveGameRoomsTest()
        {
            int roomId = 1212;
            int gameId = new Random().Next();
            int userId1 = new Random().Next();
            int userId2 = new Random().Next();
            int userId3 = new Random().Next();
            int room2Id = 123;
            int game2Id = new Random().Next();
            int user2Id1 = new Random().Next();
            int user2Id2 = new Random().Next();
            int user2Id3 = new Random().Next();
            GameRoom toAdd = CreateRoomWithId(gameId, roomId, userId1, userId2, userId3);
            GameRoom toAdd2 = CreateRoomWithId(game2Id, room2Id, user2Id1, user2Id2, user2Id3);
            toAdd.SetIsActive(true);
            proxy.InsertNewGameRoom(toAdd);
            proxy.InsertNewGameRoom(toAdd2);
            proxy.UpdateGameRoom(toAdd);
            List<XElement> g = controller.GetAllActiveGameRooms();
            bool g1 = false;
            bool g2 = false;
            foreach (var game in g)
            {
                if (proxy.GameRoomFromXElement(game).Id== roomId)
                {
                    g1 = true;
                }
                if (proxy.GameRoomFromXElement(game).Id == room2Id)
                {
                    g2 = true;
                }
            }
            Assert.AreEqual(g1, true);
            Assert.AreEqual(g2, false);
            Cleanup(gameId, roomId, userId1, userId2, userId3);
            Cleanup(game2Id, room2Id, user2Id1, user2Id2, userId3);
        }

        [TestMethod()]
        public void GetAllSpectatebleGameRoomsTest()
        {
            int roomId = 122221;
            int gameId = new Random().Next();
            int userId1 = new Random().Next();
            int userId2 = new Random().Next();
            int userId3 = new Random().Next();
            int room2Id = 122222;
            int game2Id = new Random().Next();
            int user2Id1 = new Random().Next();
            int user2Id2 = new Random().Next();
            int user2Id3 = new Random().Next();
            GameRoom toAdd = CreateRoomWithId(gameId, roomId, userId1, userId2, userId3);
            GameRoom toAdd2 = CreateRoomWithId(game2Id, room2Id, user2Id1, user2Id2, user2Id3);
            toAdd.SetDeco(0, 0, true, 1, 2, 1, GameMode.Limit, LeagueName.B);
            toAdd2.SetDeco(0, 0, false, 1, 2, 1, GameMode.Limit, LeagueName.B);
            proxy.InsertNewGameRoom(toAdd);
            proxy.InsertNewGameRoom(toAdd2);
            proxy.UpdateGameRoom(toAdd);
            List<XElement> g = controller.GetAllSpectatebleGameRooms();
            bool g1 = false;
            bool g2 = false;
            foreach (var game in g)
            {
                if (proxy.GameRoomFromXElement(game).Id == roomId)
                {
                    g1 = true;
                }
                if (proxy.GameRoomFromXElement(game).Id == room2Id)
                {
                    g2 = true;
                }
            }
            Assert.AreEqual(g1, true);
            Assert.AreEqual(g2, false);
            Cleanup(gameId, roomId, userId1, userId2, userId3);
            Cleanup(game2Id, room2Id, user2Id1, user2Id2, userId3);
        }

        [TestMethod()]
        public void GetGameRoomsByBuyInPolicyTest()
        {
            int roomId = 122221;
            int gameId = new Random().Next();
            int userId1 = new Random().Next();
            int userId2 = new Random().Next();
            int userId3 = new Random().Next();
            int room2Id = 122222;
            int game2Id = new Random().Next();
            int user2Id1 = new Random().Next();
            int user2Id2 = new Random().Next();
            int user2Id3 = new Random().Next();
            GameRoom toAdd = CreateRoomWithId(gameId, roomId, userId1, userId2, userId3);
            GameRoom toAdd2 = CreateRoomWithId(game2Id, room2Id, user2Id1, user2Id2, user2Id3);
            toAdd.SetDeco(0, 0, true, 1, 2, 222, GameMode.Limit, LeagueName.B);
            toAdd2.SetDeco(0, 0, false, 1, 2, 2222, GameMode.Limit, LeagueName.B);
            proxy.InsertNewGameRoom(toAdd);
            proxy.InsertNewGameRoom(toAdd2);
            proxy.UpdateGameRoom(toAdd);
            List<XElement> g = controller.GetGameRoomsByBuyInPolicy(222);
            bool g1 = false;
            bool g2 = false;
            foreach (var game in g)
            {
                if (proxy.GameRoomFromXElement(game).Id == roomId)
                {
                    g1 = true;
                }
                if (proxy.GameRoomFromXElement(game).Id == room2Id)
                {
                    g2 = true;
                }
            }
            Assert.AreEqual(g1, true);
            Assert.AreEqual(g2, false);
            Cleanup(gameId, roomId, userId1, userId2, userId3);
            Cleanup(game2Id, room2Id, user2Id1, user2Id2, userId3);
        }

        [TestMethod()]
        public void GetGameRoomsByGameModeTest()
        {
            int roomId = 122221;
            int gameId = new Random().Next();
            int userId1 = new Random().Next();
            int userId2 = new Random().Next();
            int userId3 = new Random().Next();
            int room2Id = 122222;
            int game2Id = new Random().Next();
            int user2Id1 = new Random().Next();
            int user2Id2 = new Random().Next();
            int user2Id3 = new Random().Next();
            GameRoom toAdd = CreateRoomWithId(gameId, roomId, userId1, userId2, userId3);
            GameRoom toAdd2 = CreateRoomWithId(game2Id, room2Id, user2Id1, user2Id2, user2Id3);
            toAdd.SetDeco(0, 0, true, 1, 2, 222, GameMode.PotLimit, LeagueName.B);
            toAdd2.SetDeco(0, 0, false, 1, 2, 2222, GameMode.Limit, LeagueName.B);
            proxy.InsertNewGameRoom(toAdd);
            proxy.InsertNewGameRoom(toAdd2);
            proxy.UpdateGameRoom(toAdd);
            List<XElement> g = controller.GetGameRoomsByGameMode(2); //pot limit
            bool g1 = false;
            bool g2 = false;
            foreach (var game in g)
            {
                if (proxy.GameRoomFromXElement(game).Id == roomId)
                {
                    g1 = true;
                }
                if (proxy.GameRoomFromXElement(game).Id == room2Id)
                {
                    g2 = true;
                }
            }
            Assert.AreEqual(g1, true);
            Assert.AreEqual(g2, false);
            Cleanup(gameId, roomId, userId1, userId2, userId3);
            Cleanup(game2Id, room2Id, user2Id1, user2Id2, userId3);
        }

        [TestMethod()]
        public void GetGameRoomsByMinPlayersTest()
        {
            int roomId = 122221;
            int gameId = new Random().Next();
            int userId1 = new Random().Next();
            int userId2 = new Random().Next();
            int userId3 = new Random().Next();
            int room2Id = 122222;
            int game2Id = new Random().Next();
            int user2Id1 = new Random().Next();
            int user2Id2 = new Random().Next();
            int user2Id3 = new Random().Next();
            GameRoom toAdd = CreateRoomWithId(gameId, roomId, userId1, userId2, userId3);
            GameRoom toAdd2 = CreateRoomWithId(game2Id, room2Id, user2Id1, user2Id2, user2Id3);
            toAdd.SetDeco(0, 0, true, 111, 999, 222, GameMode.PotLimit, LeagueName.B);
            toAdd2.SetDeco(0, 0, false, 1, 2, 2222, GameMode.Limit, LeagueName.B);
            proxy.InsertNewGameRoom(toAdd);
            proxy.InsertNewGameRoom(toAdd2);
            proxy.UpdateGameRoom(toAdd);
            List<XElement> g = controller.GetGameRoomsByMinPlayers(111);
            bool g1 = false;
            bool g2 = false;
            foreach (var game in g)
            {
                if (proxy.GameRoomFromXElement(game).Id == roomId)
                {
                    g1 = true;
                }
                if (proxy.GameRoomFromXElement(game).Id == room2Id)
                {
                    g2 = true;
                }
            }
            Assert.AreEqual(g1, true);
            Assert.AreEqual(g2, false);
            Cleanup(gameId, roomId, userId1, userId2, userId3);
            Cleanup(game2Id, room2Id, user2Id1, user2Id2, userId3);
        }

        [TestMethod()]
        public void GetGameRoomsByPotSizeTest()
        {
            int roomId = 122221;
            int gameId = new Random().Next();
            int userId1 = new Random().Next();
            int userId2 = new Random().Next();
            int userId3 = new Random().Next();
            int room2Id = 122222;
            int game2Id = new Random().Next();
            int user2Id1 = new Random().Next();
            int user2Id2 = new Random().Next();
            int user2Id3 = new Random().Next();
            GameRoom toAdd = CreateRoomWithId(gameId, roomId, userId1, userId2, userId3);
            GameRoom toAdd2 = CreateRoomWithId(game2Id, room2Id, user2Id1, user2Id2, user2Id3);
            toAdd.SetPotSize(151515);
            toAdd.SetDeco(123, 0, true, 111, 999, 222, GameMode.PotLimit, LeagueName.B);
            toAdd2.SetDeco(0, 0, false, 1, 2, 2222, GameMode.Limit, LeagueName.B);
            proxy.InsertNewGameRoom(toAdd);
            proxy.InsertNewGameRoom(toAdd2);
            proxy.UpdateGameRoom(toAdd);
            List<XElement> g = controller.GetGameRoomsByPotSize(151515);
            bool g1 = false;
            bool g2 = false;
            foreach (var game in g)
            {
                if (proxy.GameRoomFromXElement(game).Id == roomId)
                {
                    g1 = true;
                }
                if (proxy.GameRoomFromXElement(game).Id == room2Id)
                {
                    g2 = true;
                }
            }
            Assert.AreEqual(g1, true);
            Assert.AreEqual(g2, false);
            Cleanup(gameId, roomId, userId1, userId2, userId3);
            Cleanup(game2Id, room2Id, user2Id1, user2Id2, userId3);
        }

        [TestMethod()]
        public void GetGameRoomsByStartingChipTest()
        {
            int roomId = 122221;
            int gameId = new Random().Next();
            int userId1 = new Random().Next();
            int userId2 = new Random().Next();
            int userId3 = new Random().Next();
            int room2Id = 122222;
            int game2Id = new Random().Next();
            int user2Id1 = new Random().Next();
            int user2Id2 = new Random().Next();
            int user2Id3 = new Random().Next();
            GameRoom toAdd = CreateRoomWithId(gameId, roomId, userId1, userId2, userId3);
            GameRoom toAdd2 = CreateRoomWithId(game2Id, room2Id, user2Id1, user2Id2, user2Id3);
            toAdd.SetPotSize(151515);
            toAdd.SetDeco(123, 4848, true, 111, 999, 222, GameMode.PotLimit, LeagueName.B);
            toAdd2.SetDeco(0, 0, false, 1, 2, 2222, GameMode.Limit, LeagueName.B);
            proxy.InsertNewGameRoom(toAdd);
            proxy.InsertNewGameRoom(toAdd2);
            proxy.UpdateGameRoom(toAdd);
            List<XElement> g = controller.GetGameRoomsByStartingChip(4848);
            bool g1 = false;
            bool g2 = false;
            foreach (var game in g)
            {
                if (proxy.GameRoomFromXElement(game).Id == roomId)
                {
                    g1 = true;
                }
                if (proxy.GameRoomFromXElement(game).Id == room2Id)
                {
                    g2 = true;
                }
            }
            Assert.AreEqual(g1, true);
            Assert.AreEqual(g2, false);
            Cleanup(gameId, roomId, userId1, userId2, userId3);
            Cleanup(game2Id, room2Id, user2Id1, user2Id2, userId3);
        }

        [TestMethod()]
        public void GetGameRoomsByMinBetTest()
        {
            int roomId = 122221;
            int gameId = new Random().Next();
            int userId1 = new Random().Next();
            int userId2 = new Random().Next();
            int userId3 = new Random().Next();
            int room2Id = 122222;
            int game2Id = new Random().Next();
            int user2Id1 = new Random().Next();
            int user2Id2 = new Random().Next();
            int user2Id3 = new Random().Next();
            GameRoom toAdd = CreateRoomWithId(gameId, roomId, userId1, userId2, userId3);
            GameRoom toAdd2 = CreateRoomWithId(game2Id, room2Id, user2Id1, user2Id2, user2Id3);
            toAdd.SetDeco(123, 0, true, 111, 999, 222, GameMode.PotLimit, LeagueName.B);
            toAdd2.SetDeco(0, 0, false, 1, 2, 2222, GameMode.Limit, LeagueName.B);
            proxy.InsertNewGameRoom(toAdd);
            proxy.InsertNewGameRoom(toAdd2);
            proxy.UpdateGameRoom(toAdd);
            List<XElement> g = controller.GetGameRoomsByMinBet(123);
            bool g1 = false;
            bool g2 = false;
            foreach (var game in g)
            {
                if (proxy.GameRoomFromXElement(game).Id == roomId)
                {
                    g1 = true;
                }
                if (proxy.GameRoomFromXElement(game).Id == room2Id)
                {
                    g2 = true;
                }
            }
            Assert.AreEqual(g1, true);
            Assert.AreEqual(g2, false);
            Cleanup(gameId, roomId, userId1, userId2, userId3);
            Cleanup(game2Id, room2Id, user2Id1, user2Id2, userId3);
        }

        [TestMethod()]
        public void GetGameRoomsByMaxPlayersTest()
        {
            int roomId = 122221;
            int gameId = new Random().Next();
            int userId1 = new Random().Next();
            int userId2 = new Random().Next();
            int userId3 = new Random().Next();
            int room2Id = 122222;
            int game2Id = new Random().Next();
            int user2Id1 = new Random().Next();
            int user2Id2 = new Random().Next();
            int user2Id3 = new Random().Next();
            GameRoom toAdd = CreateRoomWithId(gameId, roomId, userId1, userId2, userId3);
            GameRoom toAdd2 = CreateRoomWithId(game2Id, room2Id, user2Id1, user2Id2, user2Id3);
            toAdd.SetDeco(0, 0, true, 1, 999, 222, GameMode.PotLimit, LeagueName.B);
            toAdd2.SetDeco(0, 0, false, 1, 2, 2222, GameMode.Limit, LeagueName.B);
            proxy.InsertNewGameRoom(toAdd);
            proxy.InsertNewGameRoom(toAdd2);
            proxy.UpdateGameRoom(toAdd);
            List<XElement> g = controller.GetGameRoomsByMaxPlayers(999);
            bool g1 = false;
            bool g2 = false;
            foreach (var game in g)
            {
                if (proxy.GameRoomFromXElement(game).Id == roomId)
                {
                    g1 = true;
                }
                if (proxy.GameRoomFromXElement(game).Id == room2Id)
                {
                    g2 = true;
                }
            }
            Assert.AreEqual(g1, true);
            Assert.AreEqual(g2, false);
            Cleanup(gameId, roomId, userId1, userId2, userId3);
            Cleanup(game2Id, room2Id, user2Id1, user2Id2, userId3);
        }

       

        [TestMethod()]
        public void GetGameRoomByIdTest()
        {
            bool ans = proxy.InsertNewGameRoom(gameRoom);
            XElement gacc = controller.GetGameRoomById(gameRoom.Id);
            GameRoom gac = proxy.GameRoomFromXElement(gacc);
            Assert.IsTrue(gac.Id == gameRoom.Id);
            Assert.IsTrue(gac.IsGameActive() == gameRoom.IsGameActive());
            Assert.IsTrue(gac.GetBuyInPolicy() == gameRoom.GetBuyInPolicy());
            Assert.IsTrue(gac.GetCurrPosition() == gameRoom.GetCurrPosition());
            proxy.DeleteGameRoomPref(gameRoom.Id);
            proxy.DeleteGameRoom(gameRoom.Id, gameRoom.GetGameNum());
        }

       
    }
}