using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Udp;
using Entitas;
using System;

namespace Assets.Scripts.Features.Client.Networking
{
    public class ClientSystem : IExecuteSystem, IContextInitialize, ITearDownSystem
    {
        #region Services

        private GameTimeService _gameTimeService = null;

        #endregion

        #region Factories

        private ConnectionStateFactory _connectionStateFactory = null;

        private MessageFactory _messageFactory = null;

        #endregion

        #region Fields

        private IListener
            _listener;

        private ISender
            _sender;

        private ConcurrentReceiveStack
            _messageStack = new ConcurrentReceiveStack();

        private GameEntity
            _stateEntity;

        private GameTimeEvent
            _tryTimeEvent;

        private GameTimeEvent
            _connectionTimeEvent;

        private MessageContract
            _connectContract;

        #endregion

        void IContextInitialize.Initialize(Contexts context)
        {
            _connectContract = _messageFactory.CreateConnectMessage(Guid.NewGuid());

            _tryTimeEvent = _gameTimeService.CreateTimeEvent(0.6f);
            _connectionTimeEvent = _gameTimeService.CreateTimeEvent(1);

            _stateEntity = _connectionStateFactory.Create();

            _sender = new UdpSender(new SendConfiguration
            {
                RemoteHost = "localhost",
                RemotePort = 32100
            });

            _listener = new UdpListener(new ListenConfiguration
            {
                ListeningPort = 32000,
                ReceiveTimeout = 1000
            });

            _listener.OnReceive += (endpoint, message)=> _messageStack.Push(message);

            _listener.Open();
            _sender.Open();
        }

        public void Execute()
        {
            var connectionState = _stateEntity.connectionState.value;

            bool isHasMessage = false;

            MessageContract message;

            if (_messageStack.TryPopMessage(out message))
            {
                isHasMessage = true;

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
                    if (isHasMessage)
                    {
                        _stateEntity.ReplaceConnectionState(ConnectionState.Active, 0);
                    }
                    else if (_tryTimeEvent.Check())
                    {
                        _sender.Send(_connectContract);

                        _stateEntity.ReplaceConnectionState(connectionState, _stateEntity.connectionState.tryCount + 1);
                    }
                    break;
            }
        }

        public void TearDown()
        {
            _listener.Close();

           _stateEntity.ReplaceConnectionState(ConnectionState.Closed, 0);
        }
    }
}
