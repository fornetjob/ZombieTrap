using Game.Core.Networking;
using Game.Core.Networking.Udp;
using Entitas;
using System;
using System.Net;

namespace Assets.Scripts.Features.Client.Networking
{
    public class ClientSystem : IExecuteSystem, IContextInitialize, ITearDownSystem
    {
        #region Services

        private GameTimeService _gameTimeService = null;
        private SerializerService _serializerService = null;
        private ClientMessageProcessor _messageProcessor = null;

        #endregion

        #region Fields

        private IListener
            _listener;

        private GameEntity
            _stateEntity;

        private GameTimeEvent
            _connectionTimeEvent;

        private ConcurrentReceiveStack
           _strongMessagesStack = new ConcurrentReceiveStack();

        #endregion

        #region IContextInitialize

        void IContextInitialize.Initialize(Contexts context)
        {
            _stateEntity = context.game.connectionStateEntity;

            _connectionTimeEvent = _gameTimeService.CreateTimeEvent(1);

            _listener = new UdpListener(new ListenConfiguration
                {
                    ListeningPort = 32000,
                    ReceiveInterval = 10
                });

            _listener.OnReceive += OnMessageReceive;

            _listener.Open();
        }

        #endregion

        #region Event handlers

        private void OnMessageReceive(IPEndPoint ip, MessageContract msg)
        {
            if (_stateEntity.connectionState.value != ConnectionState.Active)
            {
                _stateEntity.ReplaceConnectionState(ConnectionState.Active, 0);
            }

            _connectionTimeEvent.Reset();

            if (msg.Type.IsStrongMessage())
            {
                _strongMessagesStack.Push(msg);
            }
            else
            {
                _messageProcessor.Process(msg);
            }
        }

        #endregion

        public void Execute()
        {
            if (_connectionTimeEvent.Check()
                && _stateEntity.connectionState.value == ConnectionState.Active)
            {
                _stateEntity.ReplaceConnectionState(ConnectionState.Lost, 0);
            }
            else
            {
                MessageContract msg;

                if (_strongMessagesStack.TryPopMessage(out msg))
                {
                    _messageProcessor.Process(msg);
                }
            }
        }

        #region ITearDownSystem

        void ITearDownSystem.TearDown()
        {
            _listener.Close();

           _stateEntity.ReplaceConnectionState(ConnectionState.Closed, 0);
        }

        #endregion
    }
}
