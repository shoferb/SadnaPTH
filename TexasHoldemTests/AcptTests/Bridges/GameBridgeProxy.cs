﻿using System;
using System.Collections.Generic;

namespace TexasHoldemTests.AcptTests.Bridges
{
    class GameBridgeProxy : IGameBridge
    {
        public bool CreateGameRoom(int userId, int roomId)
        {
            return true;
        }

        public bool CreateGameRoom(int id)
        {
            return true;
        }

        public int GetNextFreeRoomId()
        {
            return 1;
        }

        public bool RemoveGameRoom(int id)
        {
            return true;
        }

        public bool DoesRoomExist(int id)
        {
            return true;
        }

        public bool IsUserInRoom(int userId, int roomId)
        {
            return true;
        }

        public bool StartGame(int roomId)
        {
            throw new NotImplementedException();
        }

        public bool IsRoomActive(int roomId)
        {
            throw new NotImplementedException();
        }

        public int GetDealerId(int roomId)
        {
            throw new NotImplementedException();
        }

        public int GetBbId(int roomId)
        {
            throw new NotImplementedException();
        }

        public int GetSbId(int roomId)
        {
            throw new NotImplementedException();
        }

        public List<int> GetPlayersInRoom(int roomId)
        {
            throw new NotImplementedException();
        }
    }
}
