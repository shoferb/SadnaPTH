﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Notifications_And_Logs;
using TexasHoldem.Logic.Users;

namespace TexasHoldem.Logic.GameControl
{
    public class LogControl
    {

        private List<ErrorLog> errorLog;
        private List<SystemLog> systemLog;

        private static readonly object padlock = new object();

        public LogControl()
        {
            this.errorLog = new List<ErrorLog>();
            this.systemLog = new List<SystemLog>();
        }

        
        public Log FindLog(int logId)
        {
            lock (padlock)
            {
                Log toReturn = null;
                bool found = false;
                foreach (SystemLog sl in systemLog)
                {
                    if (sl.LogId == logId)
                    {
                        toReturn = sl;
                        found = true;
                    }
                }
                if (!found)
                {
                    foreach (ErrorLog el in errorLog)
                    {
                        if (el.LogId == logId)
                        {
                            toReturn = el;
                            found = true;
                        }
                    }
                }
                return toReturn;
            }
        }

        public void AddSystemLog(SystemLog log)
        {
            lock (padlock)
            {
                systemLog.Add(log);
            }
        }

        public void AddErrorLog(ErrorLog log)
        {
            lock (padlock)
            {
                errorLog.Add(log);
            }
        }

        public void RemoveErrorLog(ErrorLog log)
        {
            lock (padlock)
            {
                errorLog.Remove(log);
            }
        }

        public void RemoveSystenLog(SystemLog log)
        {
            lock (padlock)
            {
                systemLog.Remove(log);
            }
        }

        public Notification CreateNotification(int roomId, string msg)
        {
            Notification toReturn = new Notification(roomId, msg);
            return toReturn;
        }

        public bool SendNotification(IUser user, int roomId, string msg)
        {
            Notification toSend = CreateNotification(roomId, msg);
            bool toReturn = user.SendNotification(toSend);
            return toReturn;
        }


        public bool SendNotificationByUserId(IUser user, int roomId, string msg)
        {
            Notification toSend = CreateNotification(roomId, msg);
            if (user == null)
            {
                return false;
            }
            bool toReturn = user.SendNotification(toSend);
            return toReturn;
        }

    }
}
