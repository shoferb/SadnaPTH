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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using Client.Logic;

namespace TexasHoldem.GuiScreen
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        private WellcomeScreen wsScreen;
        private RegisterScreen rgScreen; 
        private string userName;
        private string password;
        private ClientLogic cl;


        public LoginScreen(WellcomeScreen ws, ClientLogic cli)
        {
            InitializeComponent();
            cl = cli;
            wsScreen = ws;

        }


        private void UserNametextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            userName = UserNametextBox.Text;
        }
        private void MainMenuBtton_Click(object sender, RoutedEventArgs e)
        {
            wsScreen.Show();
            this.Hide();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            rgScreen = new RegisterScreen(this,cl);
            rgScreen.Show();
            this.Hide();
        }

        private void Loginbutton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UserNametextBox.Text))
            {
                MessageBox.Show("Invalid input - please enter username");
                return;
            }
            if ( string.IsNullOrEmpty(passwordBox.Text))
            {
                MessageBox.Show("Invalid input - please enter password");
            }
            bool loginOk = cl.Login(userName, password);
            if (loginOk )
            {
                MainAfterLogin mainAfterLogin = new MainAfterLogin(this, cl.user.id,cl);
                mainAfterLogin.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("login fail! - please try again");
            }
        }
        
        private void passwordBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Text = "";
            passwordBox.Opacity = 100;
            
        }

        private void UserNametextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            userName = UserNametextBox.Text;
        }

        private void passwordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            password = passwordBox.Text;
        }
    }
}
