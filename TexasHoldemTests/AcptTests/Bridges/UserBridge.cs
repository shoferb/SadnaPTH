﻿using System.Collections.Generic;
using TexasHoldem.Logic.Game;
using TexasHoldem.Logic.Users;
using TexasHoldem.Service;
using TexasHoldemTests.AcptTests.Bridges.Interface;

namespace TexasHoldemTests.AcptTests.Bridges
{
    class UserBridge : IUserBridge
    {
        private UserServiceHandler _userService;
        private GameServiceHandler _gameService;

        public UserBridge()
        {
            //TODO: init service here
        }

        public bool IsUserLoggedIn(int userId)
        {
            throw new System.NotImplementedException();
        }

        public string GetUserName(int id)
        {
            return _userService.GetUserFromId(id).Name;
        }

        public string GetUserPw(int id)
        {
            //return _userService.GetUserFromId(id).Password;
            return "";
        }

        public string GetUserEmail(int id)
        {
            return _userService.GetUserFromId(id).Email;
        }

        public int GetUserMoney(int id)
        {
            return _userService.GetUserFromId(id).Money;
        }

        public int GetUserChips(int userId)
        {
            throw new System.NotImplementedException();
        }

        public int GetUserChips(int userId, int roomId)
        {
            throw new System.NotImplementedException();
        }

        public List<int> GetUsersGameRooms(int userId)
        {
            List<ConcreteGameRoom> allGames = _gameService.GetAllGames();
            List<int> gameIds = new List<int>();
            allGames.ForEach(game =>
            {
                game._players.ForEach(p =>
                {
                    if (p.Id == userId)
                    {
                        gameIds.Add(game._id.GetHashCode());
                    }
                });
                //game.RoomSpectetors.ForEach(s =>
                //{
                //    if (s.Id == userId)
                //    {
                //        gameIds.Add(game.GameId);
                //    }
                //});

            });
            return gameIds;
        }

        public List<string> GetUserNotificationMsgs(int userId)
        {
            var toReturn = new List<string>();
            var notifications = _userService.GetUserNotifications(userId);
            notifications.ForEach(noti =>
            {
                toReturn.Add(noti.Msg);
            });
            return toReturn;
        }

        public int GetNextFreeUserId()
        {
            return _userService.GetNextUserId();
        }

        public int GetUserRank(int userId)
        {
            return _userService.GetUserFromId(userId).Points;
        }

        public void SetUserRank(int userId, int rank)
        {
            _userService.GetUserFromId(userId).Points = rank;
        }

        public bool SetUserRank(int userIdToChange, int rank, int changingUserId)
        {
            int changingUserRank = _userService.GetUserFromId(changingUserId).Points;
            if (changingUserRank == _userService.GetMaxUserPoints())
            {
                SetUserRank(userIdToChange, rank);
                return true;
            }
            return false;
        }

        public bool SetLeagueCriteria(int userId, int criteria)
        {
            throw new System.NotImplementedException();
        }

        public bool IsThereUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<int> GetAllUsers()
        {
            var allUsers = _userService.GetAllUsers();
            List<int> ids = new List<int>();
            allUsers.ForEach(user =>
            {
                ids.Add(user.Id);
            });
            return ids;
        }

        public bool LoginUser(string name, string password)
        {
            return _userService.LoginUser(name, password);
        }

        public bool LogoutUser(int userId)
        {
            return _userService.LogoutUser(userId);
        }

        public bool RegisterUser(string name, string pw1, string email)
        {
            int id = _userService.GetNextUserId();
            var user = _userService.CreateNewUser(id, name, name, pw1, email);
            return user != null;
        }

        public bool DeleteUser(string name, string pw)
        {
            return _userService.DeleteUser(name, pw);
        }

        public bool DeleteUser(int id)
        {
            var user = _userService.GetUserFromId(id);
            return DeleteUser(user.Name, user.Password);
        }

        public bool EditName(int id, string newName)
        {
            return _userService.EditUserName(id, newName);
        }

        public bool EditPw(int id, string oldPw, string newPw)
        {
            return _userService.EditUserPassword(id, oldPw, newPw);
        }

        public bool EditEmail(int id, string newEmail)
        {
            return _userService.EditUserEmail(id, newEmail);
        }

        public bool AddUserToGameRoomAsPlayer(int userId, int roomId, int chipAmount)
        {
            var player = _userService.GetPlayer(userId, roomId);

            if (player == null)
            {
                User user = _userService.GetUserFromId(userId);
                player = new Player(chipAmount, 0, user.Id, user.Name, user.MemberName,
                    user.Password, user.Points, user.Money, user.Email, roomId);
                var room = _gameService.GetGameById(roomId);
                return _gameService.AddPlayerToRoom(player, room); 
            }
            return false;
        }

        public bool AddUserToGameRoomAsSpectator(int userId, int roomId)
        {
            User user = _userService.GetUserFromId(userId);
            Spectetor spect = new Spectetor(userId, user.Name, user.MemberName,
                user.Password, user.Points, user.Money, user.Email, roomId);
            return _gameService.AddSpectatorToRoom(spect, _gameService.GetGameById(roomId));
        }

        public bool RemoveUserFromRoom(int userId, int roomId)
        {
            return _gameService.RemoveUserFromRoom(userId, roomId);
        }

        private bool ChangeUserMoney(int userId, int amount)
        {
            var user = _userService.GetUserFromId(userId);
            if (user != null)
            {
                user.Money += amount;
                return true;
            }
            return false;
        }

        public bool ReduceUserMoney(int userId, int amount)
        {
            return ChangeUserMoney(userId, -1 * amount);
        }

        public bool AddUserMoney(int userId, int amount)
        {
            return ChangeUserMoney(userId, amount);
        }

        public bool AddUserChips(int userId, int roomId, int amount)
        {
            var player = _userService.GetPlayer(userId, roomId);
            if (player != null)
            {
                player._chipCount += amount;
                return true;
            }
            return false;
        }
    }
}
