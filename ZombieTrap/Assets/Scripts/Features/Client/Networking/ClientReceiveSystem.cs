using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Udp;
using Entitas;
using System;

namespace Assets.Scripts.Features.Client.Networking
{
    public class ClientReceiveSystem : IExecuteSystem, IContextInitialize, ITearDownSystem
    {
        #region Services

        private GameTimeService _gameTimeService = null;

        #endregion

        #region Fields

        private IConnection
            _connection;

        private ConcurrentReceiveStack
            _messageStack = new ConcurrentReceiveStack();

        private DateTime
            _lastMessageTime;

        private GameEntity
            _stateEntity;

        private GameTimeEvent
            _tryTimeEvent;

        private GameTimeEvent
            _connectionTimeEvent;

        #endregion

        void IContextInitialize.Initialize(Contexts context)
        {
            _stateEntity = new ConnectionStateFactory(context).Create();

            _connection = new UdpConnection(new ConnectionConfiguration
            {
                RemoteHost = "localhost",
                RemotePort = 32100,
                ListeningPort = 32000,
                ReceiveTimeout = 1000
            });

            _tryTimeEvent = _gameTimeService.CreateTimeEvent(0.3f);
            _connectionTimeEvent = _gameTimeService.CreateTimeEvent(1);

            _connection.OnReceive += _listener_OnReceive;

            _connection.Open();
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
                    _stateEntity.ReplaceConnectionState(ConnectionState.Active, 0);
                }

                _connectionTimeEvent.Reset();
            }
            else if (connectionState == ConnectionState.Active
                && _connectionTimeEvent.Check())
            {
                _stateEntity.ReplaceConnectionState(ConnectionState.Lost, 0);

                _tryTimeEvent.Reset();
            }

            switch (connectionState)
            {
                case ConnectionState.Connecting:
                case ConnectionState.Lost:
                    if (_tryTimeEvent.Check())
                    {
                        _connection.Send(MessageContract.ConnectMessage);

                        _stateEntity.ReplaceConnectionState(connectionState, _stateEntity.connectionState.tryCount + 1);
                    }
                    break;
            }
        }

        public void TearDown()
        {
            _connection.Close();

           _stateEntity.ReplaceConnectionState(ConnectionState.Closed, 0);
        }
    }
}
