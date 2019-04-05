﻿using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Udp;
using Entitas;
using System;

namespace Assets.Scripts.Features.Client.Networking
{
    public class ClientReceiveSystem : IExecuteSystem, IInitializeSystem, ITearDownSystem
    {
        private const int TimeOutMs = 1000;

        private IConnection
            _listener;

        private ConcurrentReceiveStack
            _messageStack = new ConcurrentReceiveStack();

        private DateTime
            _lastMessageTime;

        private GameEntity
            _stateEntity;

        public ClientReceiveSystem(Contexts context)
        {
            _stateEntity = new ConnectionStateFactory(context).Create();
        }

        public void Initialize()
        {
            _listener = new UdpConnection(new ConnectionConfiguration
            {
                RemoteHost = "localhost",
                RemotePort = 32100,
                ListeningPort = 32000,
                ReceiveTimeout = 1000
            });

            _listener.Open();
            _listener.OnReceive += _listener_OnReceive;
        }

        private void _listener_OnReceive(MessageContract message)
        {
            _messageStack.Push(message);
        }

        public void Execute()
        {
            var connectionState = _stateEntity.connectionState.value;

            MessageContract message;

            if (_messageStack.TryPopMessage(out message))
            {
                _lastMessageTime = DateTime.Now;

                if (connectionState != ConnectionState.Active)
                {
                    _stateEntity.ReplaceConnectionState(ConnectionState.Active);
                }
            }
            else if (connectionState == ConnectionState.Active
                && (DateTime.Now - _lastMessageTime).TotalMilliseconds > TimeOutMs)
            {
                _stateEntity.ReplaceConnectionState(ConnectionState.Lost);
            }
        }

        public void TearDown()
        {
            _listener.Close();

           _stateEntity.ReplaceConnectionState(ConnectionState.Closed);
        }
    }
}
