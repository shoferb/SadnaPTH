﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TexasHoldem.communication.Interfaces;
using TexasHoldem.communication.Reactor.Interfaces;

namespace TexasHoldem.communication.Impl
{

    public class CommunicationHandler : ICommunicationHandler
    {
        private readonly int _localPort;
        protected bool ShouldClose = false;
        protected bool WasShutDown = false; //used only for testing
        private static readonly object Padlock = new object();

        protected readonly IListenerSelector Selector;
        protected readonly TcpListener _listener;
        protected readonly ConcurrentQueue<TcpClient> _socketsQueue;
        protected readonly ConcurrentQueue<string> _receivedMsgQueue;
        protected readonly IDictionary<int, ConcurrentQueue<string>> _userIdToMsgQueue;
        protected readonly IDictionary<TcpClient, int> _socketToUserId; //sockets to user ids
        protected readonly ManualResetEvent _connectionCleanerMre = new ManualResetEvent(false);
        protected List<Task> taskList;

        public CommunicationHandler(IListenerSelector selector, int port)
        {
            Selector = selector;
            _localPort = port;
            _socketsQueue = new ConcurrentQueue<TcpClient>();
            _receivedMsgQueue = new ConcurrentQueue<string>();
            _userIdToMsgQueue = new ConcurrentDictionary<int, ConcurrentQueue<string>>();
            _socketToUserId = new ConcurrentDictionary<TcpClient, int>();

            _listener = new TcpListener(IPAddress.Any, _localPort);
        }

        //start all threads:
        public void Start()
        {

            taskList = new List<Task>
            {
                new Task(HandleReading),
                new Task(HandleWriting),
                new Task(RemoveUnconnectedClients)
            };
            taskList.ForEach(task => task.Start());

            AcceptClients();
        }

        public int Port
        {
            get { return _localPort; }
        }

        public List<string> GetReceivedMessages()
        {
            return new List<string>(_receivedMsgQueue.ToArray());
        }

        public bool AddMsgToSend(string msg, int userId)
        {
            if (!_userIdToMsgQueue.ContainsKey(userId))
            {
                _userIdToMsgQueue.Add(userId, new ConcurrentQueue<string>());
            }
            var queue = _userIdToMsgQueue[userId];
            queue.Enqueue(msg);
            return true;
        }

        //main thread:
        public void AcceptClients()
        {
            //main thread so no need to signal it started
            _listener.Start();
            while (!ShouldClose)
            {
                try
                {
                    TcpClient tcpClient = _listener.AcceptTcpClient();
                    _socketsQueue.Enqueue(tcpClient);

                    _connectionCleanerMre.Set(); //wake the thread removing unconnected clients
                }
                catch (SocketException e)
                {
                    //TODO: change this to log
                    Console.WriteLine("listener socket has thrown: " + e.Message);
                }
            }
            _listener.Stop();
            ShutDown();
        }

        //thread 1
        protected void HandleReading()
        {
            byte[] buffer = new byte[1];

            while (!ShouldClose)
            {
                IList<TcpClient> readyToRead = Selector.SelectForReading(_socketsQueue);
                foreach (var tcpClient in readyToRead)
                {
                    IList<byte> data = new List<byte>();
                    NetworkStream stream = tcpClient.GetStream();
                    var dataReceived = 0;
                    do
                    {
                        dataReceived = stream.Read(buffer, 0, 1);
                        if (dataReceived > 0)
                        {
                            data.Add(buffer[0]);
                        }

                    } while (dataReceived > 0);

                    //add msg string to queue
                    if (data.Count > 0)
                    {
                        _receivedMsgQueue.Enqueue(data.ToArray().ToString());
                    }
                } 
            }
        }

        //thread 2
        protected void HandleWriting()
        {
            while (!ShouldClose)
            {
                IList<TcpClient> readyToWrite = Selector.SelectForWriting(_socketsQueue);
                foreach (var tcpClient in readyToWrite)
                {
                    if (CanSendMsg(tcpClient))
                    {
                        var msgQueue = _userIdToMsgQueue[_socketToUserId[tcpClient]]; //get msg queue
                        SendAllMsgFromQueue(msgQueue, tcpClient);
                    }
                } 
            }

            //send all remaining messages from all msg queues
            foreach (var socketToIdPair in _socketToUserId)
            {
                int userId = socketToIdPair.Value;
                var tcpClient = socketToIdPair.Key;
                var queue = _userIdToMsgQueue[userId];
                SendAllMsgFromQueue(queue, tcpClient);
            }
        }

        //true if socket and user id exist, msgQueue isn't empty and socket connected
        protected bool CanSendMsg(TcpClient client)
        {
            if (_socketToUserId.ContainsKey(client))
            {
                int id = _socketToUserId[client];
                if (_userIdToMsgQueue.ContainsKey(id))
                {
                    return client.Connected && _socketToUserId.ContainsKey(client) &&
                           !_userIdToMsgQueue[id].IsEmpty;
                }
            }
            return false;
        }

        protected void SendAllMsgFromQueue(ConcurrentQueue<String> msgQueue, TcpClient tcpClient)
        {
            while (!msgQueue.IsEmpty)
            {
                string msg;
                msgQueue.TryDequeue(out msg);
                byte[] bytesToSend = Encoding.UTF8.GetBytes(msg);
                tcpClient.GetStream().Write(bytesToSend, 0, bytesToSend.Length);
            }
        }

        //thread 3
        protected void RemoveUnconnectedClients()
        {
            while (!ShouldClose)
            {
                //allready got MRE
                _connectionCleanerMre.Reset(); //sleep until main thread wakes it up
                List<TcpClient> tempHolder = new List<TcpClient>();
                while (!_socketsQueue.IsEmpty)
                {
                    TcpClient client;
                    _socketsQueue.TryDequeue(out client);
                    if (client != null && client.Connected)
                    {
                        tempHolder.Add(client);
                    }
                }
                tempHolder.ForEach(client => _socketsQueue.Enqueue(client));
            }
            _connectionCleanerMre.Set(); //signal thread is done
        }

        protected void ShutDown()
        {
            Task.WaitAll(taskList.ToArray());
            //TODO: maybe send shoutdown msg to all clients

            //delete all sockets and connections:
            foreach (var tcpClient in _socketsQueue)
            {
                tcpClient.Close();
            }

            //TODO: log shutdown
        }

        //called from outside to stop reactor
        public void Close()
        {
            lock (Padlock)
            {
                ShouldClose = true;
                _listener.Stop();
                _connectionCleanerMre.Set(); //wake the cleaner up;
            }
        }
    }
}