﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TexasHoldem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Game_Control;
using TexasHoldem.Logic.Users;
using Moq;
using TexasHoldem.communication.Impl;
using TexasHoldem.Logic.GameControl;
using TexasHoldem.Logic.Replay;

namespace TexasHoldem.Service.Tests
{
    [TestClass()]
    public class UserServiceHandlerTests
    {
        private SystemControl sc;
        private LogControl logs;
        private GameCenter games;
        private ReplayManager replays;
        private UserServiceHandler userService;
        private static SessionIdHandler sender;
        private void Init()
        {
            logs = new LogControl();
            sc = new SystemControl(logs);
            replays = new ReplayManager();
            sender = new SessionIdHandler();


        games = new GameCenter(sc, logs, replays,sender);
            userService = new UserServiceHandler(games, sc);
            sc.Users = new List<Logic.Users.IUser>();
        }

        [TestMethod()]
        public void RegisterToSystemTest_good()
        {
            Init();
            Assert.IsTrue(userService.RegisterToSystem(789987, "orelie", "orelie789987", "123456789", 15000, "orelie@post.bgu.ac.il"));
            userService.DeleteUserById(789987);

        }

        [TestMethod()]
        public void RegisterToSystemTest_bad_id_taken()
        {
            Init();
            userService.RegisterToSystem(95959595, "orelie", "orelie95959595", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsFalse(userService.RegisterToSystem(95959595, "orelie-notTaken", "orelie2", "123456789", 15000, "orelie@post.bgu.ac.il"));
            userService.DeleteUserById(95959595);
        }

        [TestMethod()]
        public void RegisterToSystemTest_bad_userName_taken()
        {
            Init();
            userService.RegisterToSystem(95959596, "orelie", "orelie95959596", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsFalse(userService.RegisterToSystem(959595957, "orelie", "orelie95959596", "123456789", 15000, "orelie@post.bgu.ac.il"));
            userService.DeleteUserById(95959596);
     
        }

        [TestMethod()]
        public void RegisterToSystemTest_bad_Not_Valid_email()
        {
            Init();
            Assert.IsFalse(userService.RegisterToSystem(84848485, "orelie", "orelie84848485", "123456789", 15000, "oreliepost.bgu.ac.il"));
        }

        [TestMethod()]
        public void RegisterToSystemTest_bad_Not_Valid_passWord()
        {
            Init();
            Assert.IsFalse(userService.RegisterToSystem(465465487, "orelie", "orelie465465487", "123", 15000, "orelie@post.bgu.ac.il"));
        }

        [TestMethod()]
        public void RegisterToSystemTest_bad_Not_Valid_Name()
        {
            Init();
            Assert.IsFalse(userService.RegisterToSystem(59595009, " ", "orelie59595009", "123456789", 15000, "orelie@post.bgu.ac.il"));
        }

        [TestMethod()]
        public void RegisterToSystemTest_bad_Not_Valid_Id()
        {
            Init();
            Assert.IsFalse(userService.RegisterToSystem(-1, "orelie", "orelie26-1", "123456789", 15000, "orelie@post.bgu.ac.il"));
        }

        [TestMethod()]
        public void RegisterToSystemTest_bad_Not_Valid_money()
        {
            Init();
            Assert.IsFalse(userService.RegisterToSystem(598432, "orelie", "orelie598432", "123456789", -10, "orelie@post.bgu.ac.il"));
        }

        [TestMethod()]
        public void LoginUserTest_good()
        {
            Init();
            userService.RegisterToSystem(585858524, "orelie", "orelie585858524", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsTrue(userService.LoginUser("orelie585858524", "123456789").IsLogin());
            userService.DeleteUserById(585858524);
        }

        [TestMethod()]
        public void LoginUserTest_bad_username()
        {
            Init();
            userService.RegisterToSystem(484564527, "orelie", "orelie484564527", "123456789", 15000, "orelie@post.bgu.ac.il");
            userService.LogoutUser(484564527);
            Assert.IsFalse(userService.LoginUser("orelie484564527", "123456789").IsLogin());
            userService.DeleteUserById(484564527);
        }

        [TestMethod()]
        public void LoginUserTest_bad_password()
        {
            Init();
            userService.RegisterToSystem(94949042, "orelie", "orelie94949042", "123456789", 15000, "orelie@post.bgu.ac.il");
            userService.LogoutUser(94949042);
            Assert.IsFalse(userService.LoginUser("orelie94949042", "123s56789").IsLogin());
            userService.DeleteUserById(94949042);
        }

        [TestMethod()]
        public void LogoutUserTest_good()
        {
            Init();
            userService.RegisterToSystem(48653256, "orelie", "orelie48653256", "123456789", 15000, "orelie@post.bgu.ac.il");
            userService.LoginUser("orelie48653256", "123456789");
            Assert.IsTrue(userService.LogoutUser(48653256).IsLogin());
            userService.DeleteUserById(48653256);
        }

        [TestMethod()]
        public void LogoutUserTest_Bad_no_User()
        {
            Init();
            Assert.IsFalse(userService.LogoutUser(95984254).IsLogin());
        }

        [TestMethod()]
        public void LogoutUserTest_Bad_Id()
        {
            Init();
            userService.RegisterToSystem(875875873, "orelie", "orelie875875873", "123456789", 15000, "orelie@post.bgu.ac.il");
            userService.LoginUser("orelie875875873", "123456789");
            Assert.IsFalse(userService.LogoutUser(875875874).IsLogin());
            userService.DeleteUserById(875875873);
        }

        [TestMethod()]
        public void DeleteUserTest_good()
        {
            Init();
            userService.RegisterToSystem(87564587, "orelie", "orelie87564587", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsTrue(userService.DeleteUser("orelie87564587", "123456789"));
            userService.DeleteUserById(87564587);
        }


        [TestMethod()]
        public void DeleteUserTest_Bad_user_name()
        {
            Init();
            userService.RegisterToSystem(5858412, "orelie", "orelie5858412", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsFalse(userService.DeleteUser("orelie58584no", "123456789"));
            userService.DeleteUserById(5858412);
        }


        [TestMethod()]
        public void DeleteUserTest_Bad_password()
        {
            Init();
            userService.RegisterToSystem(777888558, "orelie", "orelie777888558", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsFalse(userService.DeleteUser("orelie777888558", "1234567s9"));
            userService.DeleteUserById(777888558);
        }

        [TestMethod()]
        public void DeleteUserByIdTest_good()
        {
            Init();
            userService.RegisterToSystem(99663356, "orelie", "orelie99663356", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsTrue(userService.DeleteUserById(99663356));
        }


        [TestMethod()]
        public void DeleteUserByIdTest_bad_no_user()
        {
            Init();
            Assert.IsFalse(userService.DeleteUserById(000000));
        }

        [TestMethod()]
        public void DeleteUserByIdTest_bad_inValid_id()
        {
            Init();
            Assert.IsFalse(userService.DeleteUserById(-2000));
        }

        [TestMethod()]
        public void EditUserPointsTest_good()
        {
            Init();
            userService.RegisterToSystem(778854580, "orelie", "orelie778854580", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsTrue(userService.EditUserPoints(778854580, 778854850));
            userService.DeleteUserById(778854580);
        }

        [TestMethod()]
        public void EditUserPointsTest_Bad_no_user()
        {
            Init();
            Assert.IsFalse(userService.EditUserPoints(0050505, 884858523));
        }

        [TestMethod()]
        public void EditUserPointsTest_Bad_Ivalid_points()
        {
            Init();
            userService.RegisterToSystem(595596522, "orelie", "orelie595596522", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsFalse(userService.EditUserPoints(595596522, -100));
            userService.DeleteUserById(595596522);
        }
        //todo FROM HERE!!!!!!
        [TestMethod()]
        public void EditUserPasswordTest_good()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsTrue(userService.EditUserPassword(305077901, "or123456"));
        }

        [TestMethod()]
        public void EditUserPasswordTest_bad_no_user()
        {
            Init();
            Assert.IsFalse(userService.EditUserPassword(305077901, "or123456"));
        }

        [TestMethod()]
        public void EditUserPasswordTest_Bad_invalid_Password_small()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsFalse(userService.EditUserPassword(305077901, "or"));
        }


        [TestMethod()]
        public void EditUserPasswordTest_Bad_invalid_Password_empty()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsFalse(userService.EditUserPassword(305077901, " "));
        }

        [TestMethod()]
        public void EditUserEmailTest_good()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsTrue(userService.EditUserEmail(305077901, "or123456@wall.co.il"));
        }

        [TestMethod()]
        public void EditUserEmailTest_Bad_invlid_email()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsFalse(userService.EditUserEmail(305077901, "or123456wall.co.il"));
        }


        [TestMethod()]
        public void EditUserEmailTest_Bad_invlid_empty()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsFalse(userService.EditUserEmail(305077901, " "));
        }

        [TestMethod()]
        public void EditUserNameTest_good()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsTrue(userService.EditUserName(305077901, "NewUserName"));
        }

        [TestMethod()]
        public void EditUserNameTest_Bad_userName_taken()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            userService.RegisterToSystem(305077902, "orelie", "orelie2", "123456789", 15000, "orelie@post.bgu.ac.il");

            Assert.IsFalse(userService.EditUserName(305077902, "orelie26"));
        }

        [TestMethod()]
        public void EditUserNameTest_Bad_userName_empty()
        {
            Init();
            userService.RegisterToSystem(305077902, "orelie", "orelie2", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsFalse(userService.EditUserName(305077902, " "));
        }

        [TestMethod()]
        public void EditUserNameTest_Bad_no_user()
        {
            Init();
            Assert.IsFalse(userService.EditUserName(305077902, "orelie"));
        }

        [TestMethod()]
        public void EditNameTest_good()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsTrue(userService.EditName(305077901, "NewName"));
        }

        [TestMethod()]
        public void EditNameTest_Bad_name_empty()
        {
            Init();
            userService.RegisterToSystem(305077902, "orelie", "orelie2", "123456789", 15000, "orelie@post.bgu.ac.il");

            Assert.IsFalse(userService.EditName(305077902, " "));
        }

        [TestMethod()]
        public void EditNameTest_Bad_No_user()
        {
            Init();
            Assert.IsFalse(userService.EditName(305077902, "orelie"));
        }

        [TestMethod()]
        public void EditIdTest_good()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsTrue(userService.EditId(305077901, 305077902));
        }

        [TestMethod()]
        public void EditIdTest_bad_id_taken()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            userService.RegisterToSystem(305077902, "orelie", "orelie2", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsFalse(userService.EditId(305077901, 305077902));
        }

        [TestMethod()]
        public void EditIdTest_bad_no_user()
        {
            Init();
            Assert.IsFalse(userService.EditId(305077901, 305077902));
        }

        [TestMethod()]
        public void EditIdTest_bad_invalid_id()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsFalse(userService.EditId(305077901, -1));
        }

        [TestMethod()]
        public void EditMoneyTest_good()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsTrue(userService.EditMoney(305077901, 800));
        }

        [TestMethod()]
        public void EditMoneyTest_bad_no_user()
        {
            Init();
            Assert.IsFalse(userService.EditMoney(305077901, 800));
        }

        [TestMethod()]
        public void EditMoneyTest_bad_invalidMoney()
        {
            Init();
            Assert.IsFalse(userService.EditMoney(305077901, -800));
        }



        [TestMethod()]
        public void EditUserAvatarTest()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsTrue(userService.EditUserAvatar(305077901, "newPic"));
        }


        [TestMethod()]
        public void GetIUserByUserNameTest()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            IUser u = userService.GetIUserByUserName("orelie26");
            Assert.IsTrue(sc.Users.Contains(u));
        }

        [TestMethod()]
        public void GetAllUserTest_good()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            Assert.IsTrue(userService.GetAllUser().Count == 1);
        }

        [TestMethod()]
        public void GetUserByIdTest_good()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            IUser u = userService.GetUserById(305077901);
            Assert.IsTrue(sc.Users.Contains(u));
        }

        [TestMethod()]
        public void GetUserLeagueTest_good()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            IUser u = userService.GetUserById(305077901);
            Assert.AreEqual(u.GetLeague(), LeagueName.Unknow);
        }

        [TestMethod()]
        public void GetUserLeagueTest_bad()
        {
            Init();
            userService.RegisterToSystem(305077901, "orelie", "orelie26", "123456789", 15000, "orelie@post.bgu.ac.il");
            IUser u = userService.GetUserById(305077901);
            Assert.AreNotEqual(u.GetLeague(), LeagueName.A);
        }

        [TestMethod()]
        public void DevideLeagueTest_good()
        {
            Init();
            string name = "";
            for (int i = 1; i < 11; i++)
            {
                name = "" + i;
                userService.RegisterToSystem(i, "orelie", name, "123456789", 15000, "orelie@post.bgu.ac.il");
                userService.EditUserPoints(i, i);
            }
            Assert.IsTrue(userService.DevideLeague());

        }

        [TestMethod()]
        public void DevideLeagueTest_good_check_league_A()
        {
            Init();
            string name = "";
            for (int i = 1; i < 11; i++)
            {
                name = "" + i;
                userService.RegisterToSystem(i, "orelie", name, "123456789", 15000, "orelie@post.bgu.ac.il");
                userService.EditUserPoints(i, i);
            }

            for (int i = 1; i < 11; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    userService.GetUserById(i).IncGamesPlay();
                }
            }
            userService.DevideLeague();
            Assert.AreEqual(userService.GetUserById(10).GetLeague(), LeagueName.A);

        }
        [TestMethod()]
        public void DevideLeagueTest_good_check_league_C()
        {
            Init();
            string name = "";
            for (int i = 1; i < 11; i++)
            {
                name = "" + i;
                userService.RegisterToSystem(i, "orelie", name, "123456789", 15000, "orelie@post.bgu.ac.il");
                userService.EditUserPoints(i, i);
            }
            for (int i = 1; i < 11; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    userService.GetUserById(i).IncGamesPlay();
                }
            }
            userService.DevideLeague();
            Assert.AreEqual(userService.GetUserById(6).GetLeague(), LeagueName.C);

        }
        [TestMethod()]
        public void DevideLeagueTest_good_check_league_B()
        {
            Init();
            string name = "";
            for (int i = 1; i < 11; i++)
            {
                name = "" + i;
                userService.RegisterToSystem(i, "orelie", name, "123456789", 15000, "orelie@post.bgu.ac.il");
                userService.EditUserPoints(i, i);
            }
            for (int i = 1; i < 11; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    userService.GetUserById(i).IncGamesPlay();
                }
            }
            userService.DevideLeague();
            Assert.AreEqual(userService.GetUserById(8).Points(), 8);
            Assert.AreEqual(userService.GetUserById(8).GetLeague(), LeagueName.B);

        }
        [TestMethod()]
        public void DevideLeagueTest_good_check_league_D()
        {
            Init();
            string name = "";
            for (int i = 1; i < 11; i++)
            {
                name = "" + i;
                userService.RegisterToSystem(i, "orelie", name, "123456789", 15000, "orelie@post.bgu.ac.il");
                userService.EditUserPoints(i, i);
            }
            for (int i = 1; i < 11; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    userService.GetUserById(i).IncGamesPlay();
                }
            }
            userService.DevideLeague();
            Assert.AreEqual(userService.GetUserById(3).GetLeague(), LeagueName.D);

        }
        [TestMethod()]
        public void DevideLeagueTest_good_check_league_E()
        {
            Init();
            string name = "";
            for (int i = 1; i < 11; i++)
            {
                name = "" + i;
                userService.RegisterToSystem(i, "orelie", name, "123456789", 15000, "orelie@post.bgu.ac.il");
                userService.EditUserPoints(i, i);
            }
            for (int i = 1; i < 11; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    userService.GetUserById(i).IncGamesPlay();
                }
            }
            userService.DevideLeague();
            Assert.AreEqual(userService.GetUserById(1).GetLeague(), LeagueName.E);

        }
    }
}