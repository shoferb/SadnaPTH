﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Client.GuiScreen;
using Client.Logic;

namespace TexasHoldem.GuiScreen
{
    /// <summary>
    /// Interaction logic for MainAfterLogin.xaml
    /// </summary>
    public partial class MainAfterLogin : Window
    {
        private ClientLogic cl;
        private Window parent;
        private LogoutScreen logout;
        private int currUserId;
        public MainAfterLogin(Window Parent,int id, ClientLogic cli)
        {
            InitializeComponent();
            cl = cli;      
            parent = Parent;
           
        }

        private void Logoututton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you Sure you want To logout?", "LogoutFromSystem", MessageBoxButton.YesNoCancel);
            bool done = false;
            while (!done)
            {
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        string username = cl.user.username;
                        string password = cl.user.password;
                        bool logoutOk = cl.Logout(username, password);
                        if (logoutOk)
                        {
                            MessageBox.Show("Logout OK!");
                            done = true;
                            WellcomeScreen wellcomeScreen = new WellcomeScreen();

                            wellcomeScreen.Show();
                            this.Close();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Logout Fail! - please try again");
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        done = true;
                        break;
                }
            }
           
       
        }

        public void SetCurrId(int newId)
        {
            this.currUserId = newId;
        }

        
        private void EditUserbutton_Click(object sender, RoutedEventArgs e)
        {
            EditUserInfo editUserInfo = new EditUserInfo(this,cl);
            editUserInfo.Show();
            this.Hide();
        }

        private void GameSearchMenuutton_Click(object sender, RoutedEventArgs e)
        {
            SearchScreen searchScreen = new SearchScreen(this,cl);
            searchScreen.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateNewRoom newRoomWindow = new CreateNewRoom(this, cl);
            newRoomWindow.Show();
        }

        private void UserInfobButton_Click(object sender, RoutedEventArgs e)
        {
            UserInfoScreen userIngoScreen = new UserInfoScreen(this, cl);
            userIngoScreen.Show();
            this.Hide();
        }
    }
}
