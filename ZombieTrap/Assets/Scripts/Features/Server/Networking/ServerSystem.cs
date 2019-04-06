using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Messages;
using Assets.Scripts.Core.Networking.Udp;
using Assets.Scripts.Features.Server.Room;
using Entitas;
using System;
using System.Net;
using UnityEngine;

namespace Assets.Scripts.Features.Server.Networking
{
    public class ServerSystem : IExecuteSystem, IContextInitialize
    {
        #region Services

        private SerializerService _serializerService = null;
        private MessageService _messageService = null;

        #endregion

        #region Factories

        private PlayerFactory _playerFactory = null;
        private RoomFactory _roomFactory = null;

        #endregion

        #region Groups

        [Group(ServerSideComponentsLookup.Player)]
        private IGroup<ServerSideEntity> _players = null;

        #endregion

        private IListener
            _listener;

        void IContextInitialize.Initialize(Contexts context)
        {
            // Создадим комнату
            _roomFactory.Create();

            _listener = new UdpListener(_serializerService,
                new ListenConfiguration
                {
                    ListeningPort = 32100,
                    ReceiveInterval = 10
                });

            _listener.OnReceive += OnMessageReceive;

            _listener.Open();
        }

        public void Execute()
        {
        }

        private void OnMessageReceive(IPEndPoint ip, MessageContract msg)
        {
            switch (msg.Type)
            {
                case MessageType.Connect:
                    OnConnectMessage(ip, _messageService.ConvertToConnectMessage(msg));
                    break;
            }
        }

        private void OnConnectMessage(IPEndPoint ip, ConnectMessage msg)
        {
            var player = GetPlayer(msg.PlayerId);

            if (player == null)
            {
                _playerFactory.Create(msg.PlayerId, ip);
            }
        }

        private ServerSideEntity GetPlayer(Guid id)
        {
            if (_players.count > 0)
            {
                var entities = _players.GetEntities();

                for (int i = 0; i < entities.Length; i++)
                {
                    var entity = entities[i];

                    if (entity.player.PlayerId == id)
                    {
                        return entity;
                    }
                }
            }

            return null;
        }
    }
}