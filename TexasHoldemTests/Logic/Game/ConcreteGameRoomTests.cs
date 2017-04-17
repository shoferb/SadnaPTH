﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TexasHoldem.Logic.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Users;

namespace TexasHoldem.Logic.Game.Tests
{
    [TestClass()]
    public class ConcreteGameRoomTests
    {
        private ConcreteGameRoom _gameRoom;
        private static List<Player> _players;
        private Player _A;
        private Player _B;
        private Player _C;
        private Player _D;

        [TestInitialize()]
        public void Initialize()
        {
            _A = new Player(1000, 100, 1, "Yarden", "Chen", "", 0, 0, "", 0);
            _B = new Player(500, 100, 2,"Aviv","G","",0,0,"",0);
            _C = new Player(500, 100, 2, "Yarden2", "G", "", 0, 0, "", 0);
            _D = new Player(5000, 100, 2, "Aviv2", "G", "", 0, 0, "", 0);
            _players = new List<Player>();
            _players.Add(_A);
            _players.Add(_B);
            _players.Add(_C);
            _players.Add(_D);
            _gameRoom = new ConcreteGameRoom(_players, 50, 1, new Replay.ReplayManager());
           
        }
      
       [TestMethod()]
        public void UpdateGameState4PlayersTest()
        {
            _gameRoom._gameManager.SetRoles();
            Player curr = _gameRoom._gameManager._currentPlayer;
            Player sb = _gameRoom._gameManager._sbPlayer;
            Player bb = _gameRoom._gameManager._bbPlayer;
            Player dealer = _gameRoom._gameManager._dealerPlayer;
            _gameRoom.UpdateGameState();
            MoveRoles();
            Assert.IsTrue(curr != _gameRoom._gameManager._currentPlayer);
            Assert.IsTrue(sb != _gameRoom._gameManager._sbPlayer);
            Assert.IsTrue(bb != _gameRoom._gameManager._bbPlayer);
            Assert.IsTrue(dealer != _gameRoom._gameManager._dealerPlayer);

        }

        [TestMethod()]
        public void UpdateGameState3PlayersTest()
        {// the _bb is the curr player
            _gameRoom._players.Remove(_D);
            _gameRoom._gameManager.SetRoles();
            Player curr = _gameRoom._gameManager._currentPlayer;
            Player sb = _gameRoom._gameManager._sbPlayer;
            Player bb = _gameRoom._gameManager._bbPlayer;
            Player dealer = _gameRoom._gameManager._dealerPlayer;
            Assert.IsTrue(curr == bb);
            _gameRoom.UpdateGameState();
            //the dealer is the curr
            MoveRoles();
            Assert.IsTrue(curr != _gameRoom._gameManager._currentPlayer);
            Assert.IsTrue(sb != _gameRoom._gameManager._sbPlayer);
            Assert.IsTrue(bb != _gameRoom._gameManager._bbPlayer);
            Assert.IsTrue(dealer != _gameRoom._gameManager._dealerPlayer);

        }

        [TestMethod()]
        public void ClearPublicCardsTest()
        {
            _gameRoom.ClearPublicCards();
            Assert.IsTrue(_gameRoom._publicCards.Count == 0);
        }

        [TestMethod()]
        public void AddNewPublicCardTest()
        {
            _gameRoom._deck = new Deck();
            _gameRoom.AddNewPublicCard();
           Assert.IsTrue(_gameRoom._publicCards.Count > 0);
        }

       [TestMethod()]
        public void EndTurnTest()
       {
           
            _gameRoom.EndTurn();
            foreach (Player p in _gameRoom._players)
            {
                if (p.isPlayerActive)
                Assert.IsTrue(p._lastAction.Equals(""));
            }
            
        }

        

        [TestMethod()]
        public void newSplitPotTest()
        {
            Assert.IsTrue(_gameRoom.newSplitPot(_A));
        }

        private void MoveRoles()
        {
            _gameRoom._gameManager._currentPlayer = _gameRoom.NextToPlay();
            _gameRoom._gameManager._dealerPlayer =
                           this._gameRoom._players[
                               (this._gameRoom._players.IndexOf(_gameRoom._gameManager._dealerPlayer) + 1) % this._gameRoom._players.Count];
            _gameRoom._gameManager._sbPlayer =
                this._gameRoom._players[
                    (this._gameRoom._players.IndexOf(_gameRoom._gameManager._sbPlayer) + 1) % this._gameRoom._players.Count];
            _gameRoom._gameManager._bbPlayer =
                this._gameRoom._players[
                    (this._gameRoom._players.IndexOf(_gameRoom._gameManager._bbPlayer) + 1) % this._gameRoom._players.Count];
        }
    }
}