﻿namespace TexasHoldemShared.CommMessages.ServerToClient
{
    class UserStaticticsResponseCommMessage : ResponeCommMessage
    {
        public int AvgCashGain;
        public int AvgGrossProfit;

        public UserStaticticsResponseCommMessage(int id, bool success, CommunicationMessage originalMsg, int avgCashGain, int avgGrossProfit) 
            : base(id, success, originalMsg)
        {
            AvgCashGain = avgCashGain;
            AvgGrossProfit = avgGrossProfit;
        }

        public override string Handle(IEventHandler handler)
        {
            return handler.HandleEvent(this);
        }

        public override bool Equals(CommunicationMessage other)
        {
            bool ans = false;
            if (other.GetType() == typeof(UserStaticticsResponseCommMessage))
            {
                var afterCast = (UserStaticticsResponseCommMessage)other;
                ans = base.Equals(afterCast) && AvgCashGain == afterCast.AvgCashGain &&
                      AvgGrossProfit == afterCast.AvgGrossProfit;
            }
            return ans;
        }
    }
}
