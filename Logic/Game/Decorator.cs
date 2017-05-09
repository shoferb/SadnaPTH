﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Game;

namespace TexasHoldem.Logic
{
    public interface Decorator
    {

        void SetNextDecorator(Decorator d);
        bool CanStartTheGame(int numOfPlayers);
        bool CanSpectatble();
        int GetMinBetInRoom();
        int GetEnterPayingMoney();
        int GetStartingChip();
        int GetMaxAllowedRaise(int maxCommited, GameRoom.HandStep step);
        int GetMinAllowedRaise(int maxCommited, GameRoom.HandStep step);
        bool CanRaise(int currentPlayerBet, int maxBetInRound, GameRoom.HandStep step);
        bool CanJoin(int count, int amount);
   
        //return true the game mode is the same
        bool IsGameModeEqual(GameMode gm);

        //return true the buyIn is the same
        bool IsGameBuyInPolicyEqual(int buyIn);

        //return true the min player is the same
        bool IsGameMinPlayerEqual(int min);

        //return true the max player is the same
        bool IsGameMaxPlayerEqual(int max);

        //return true the min bet in room is the same
        bool IsGameMinBetEqual(int nimBet);

        //return true the stsrtingchip of room is the same
        bool IsGameStartingChipEqual(int startingChip);

        //return true if user has money for buyIn + starting chip and his point are in:
        bool CanUserJoinGameWithMoney(int userMoney);

        //return true can add another player
        bool CanAddAnotherPlayer(int currNumOfPlayer);
    }
}
