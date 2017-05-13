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
using TexasHoldem;
using TexasHoldemShared;
using TexasHoldemShared.CommMessages.ServerToClient;

namespace Client.GuiScreen
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Window
    {
        public int RoomId;
        public Card[] PlayerCards = new Card[2];
        public List<Card> TableCards;
        public int TotalChips;
        public int PotSize;
        public List<string> AllPlayerNames;
        public string DealerName;
        public string BbName;
        public string SbName;
        public string CurrPlayerTurn;


        public GameScreen()
        {
            InitializeComponent();
        }

        public void updateGame(GameDataCommMessage msg)
        {
            this.RoomId = msg.RoomId;
            this.SbName = msg.SbName;
            this.AllPlayerNames = msg.AllPlayerNames;
            this.BbName = msg.BbName;
            this.CurrPlayerTurn = msg.CurrPlayerTurn;
            this.DealerName = msg.DealerName;
            this.PlayerCards = msg.PlayerCards;
            this.PotSize = msg.PotSize;
            this.TableCards = msg.TableCards;
            this.TotalChips = msg.TotalChips;
        }
    }
}