﻿using Game.Core.Networking;
using Game.Core.Networking.Udp;

using Entitas;

using System.Net;
using Assets.Scripts.Features.Networking;
using Game.Core.Networking.Messages;
using UnityEngine;

public class ProcessMessagesSystem : IExecuteSystem, IContextInitialize, ITearDownSystem
{
    #region Services

    private GameTimeService _gameTimeService = null;
    private NetworkSettingsService _networkSettingsService = null;
    private MessageService _messageService = null;

    #endregion

    #region Factories

    private ItemFactory _itemFactory = null;
    private MessageFactory _messageFactory = null;
    private RoomFactory _roomFactory = null;

    #endregion

    #region Poolings

    private SenderMessagesPooling _senderMessagesPooling = null;
    private ListenMessagesPooling _listenMessagePoolings = null;

    #endregion

    #region Fields

    private Contexts
        _context;

    private IListener
        _listener;

    private GameEntity
        _stateEntity;

    private GameTimeEvent
        _connectionTimeEvent;

    #endregion

    #region IContextInitialize

    void IContextInitialize.Initialize(Contexts context)
    {
        _context = context;

        _stateEntity = context.game.connectionStateEntity;

        _connectionTimeEvent = _gameTimeService.CreateTimeEvent(1);

        _listener = new UdpListener(_networkSettingsService.GetListenConfiguration());

        _listener.OnReceive += OnMessageReceive;

        _listener.Open();
    }

    #endregion

    #region ITearDownSystem

    void ITearDownSystem.TearDown()
    {
        _listener.Close();

        _stateEntity.ReplaceConnectionState(ConnectionState.Closed, 0);
    }

    #endregion

    #region Event handlers

    private void OnMessageReceive(IPEndPoint ip, MessageContract msg)
    {
        if (_stateEntity.connectionState.value != ConnectionState.Active)
        {
            // Не читаем сообщения, пока не было синхронизировано время с сервером
            if (_stateEntity.connectionState.value == ConnectionState.Connecting
                && msg.Type != MessageType.ServerSync)
            {
                return;
            }
            else
            {
                _stateEntity.ReplaceConnectionState(ConnectionState.Active, 0);
            }
        }

        _connectionTimeEvent.Reset();

        _listenMessagePoolings.Enqueue(msg);

        if (msg.Type.IsStrongMessage())
        {
            _messageFactory.CreateReplyMessage(msg.Id);
        }
    }

    #endregion

    public void Execute()
    {
        if (_connectionTimeEvent.Check()
            && _stateEntity.connectionState.value == ConnectionState.Active)
        {
            _stateEntity.ReplaceConnectionState(ConnectionState.Lost, 0);

            // Мы потеряли соединение, очистим очередь
            _listenMessagePoolings.Clear();
            _senderMessagesPooling.Clear();
        }
        else
        {
            MessageContract msg;

            if (_listenMessagePoolings.TryDequeueMessage(out msg))
            {
                Process(msg);
            }
        }
    }

    #region Private methods

    private void Process(MessageContract msg)
    {
        switch (msg.Type)
        {
            case MessageType.ServerSync:
                OnRoomMessage(_messageService.ConvertToRoomMessage(msg));
                break;
            case MessageType.Items:
                OnItemsMessage(_messageService.ConvertToItemsMessage(msg));
                break;
            case MessageType.Positions:
                OnPositionMessage(_messageService.ConvertToPositionsMessage(msg));
                break;
            case MessageType.Damage:
                OnDamageMessage(_messageService.ConvertToDamageMessage(msg));
                break;
            default:
                throw new System.NotSupportedException(msg.Type.ToString());
        }
    }

    private void OnRoomMessage(ServerSyncMessage msg)
    {
        _gameTimeService.SyncTime(msg.ServerTime);

        if (_context.game.hasRoom == false)
        {
            _roomFactory.Create(msg.RoomNumber);
        }
        else
        {
            _context.game.ReplaceRoom(msg.RoomNumber);
        }
    }

    private void OnDamageMessage(DamageMessage msg)
    {
        for (int i = 0; i < msg.Identities.Length; i++)
        {
            var id = msg.Identities[i];
            var health = msg.Healths[i];

            var item = _context.game.GetEntityWithIdentity(id);

            if (item != null)
            {
                item.ReplaceHealth(health, new Vector3(msg.HitPos.x, 0, msg.HitPos.y));
            }
        }
    }

    private void OnItemsMessage(ItemsMessage msg)
    {
        for (int i = 0; i < msg.Identities.Length; i++)
        {
            var id = msg.Identities[i];

            var item = _context.game.GetEntityWithIdentity(id);

            if (item == null)
            {
                item = _itemFactory.Create(id, msg.Types[i], msg.Radiuses[i], msg.Speeds[i], msg.Positions[i], msg.Healths[i], msg.WaitTo[i]);
            }
        }
    }

    private void OnPositionMessage(PositionsMessage msg)
    {
        for (int i = 0; i < msg.Identities.Length; i++)
        {
            var id = msg.Identities[i];

            var enemy = _context.game.GetEntityWithIdentity(id);

            if (enemy != null)
            {
                var floatPos = msg.Positions[i];
                var position = new Vector3(floatPos.x, 0, floatPos.y);

                if (enemy.position.value != position)
                {
                    var dir = (position - enemy.position.value).normalized;

                    enemy.ReplaceMove(dir, position, enemy.item.speed);
                }
            }
        }
    }

    #endregion
}