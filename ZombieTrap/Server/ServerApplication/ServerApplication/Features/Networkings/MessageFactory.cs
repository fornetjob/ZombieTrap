using Game.Core;
using Game.Core.Networking;
using Game.Core.Networking.Messages;
using ServerApplication.Features.Items;
using System;
using System.Collections.Generic;

public class MessageFactory : IDependency
{
    #region Services

    private RoomBoundService _roomBoundService = null;
    private SerializerService _serializerService = null;

    #endregion

    #region Poolings

    private ItemsPooling _itemsPooling = null;
    private PlayersPooling _playersPooling = null;
    private MessagePooling _messagePooling = null;

    #endregion

    #region Fields

    private ulong
        _messageId = 0;

    #endregion

    public void CreatePositionsMessage(Guid roomId)
    {
        var items = _itemsPooling.Get(roomId);

        var posMsg = new PositionsMessage
        {
            Identities = new ulong[items.Count],
            Positions = new Vector2Float[items.Count]
        };

        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];

            posMsg.Identities[i] = item.ItemId;
            posMsg.Positions[i] = item.Pos;
        }

        byte[] data = _serializerService.Serialize(posMsg);

        AddMessageToAllRoomPlayers(roomId, MessageType.Positions, data);
    }

    public void CreateItemMessage(Guid roomId, Item item)
    {
        var items = new List<Item>
        {
            item
        };

        CreateItemsMessage(roomId, items);
    }

    public void CreateItemsMessage(Guid roomId, Guid? playerId = null)
    {
        CreateItemsMessage(roomId, _itemsPooling.Get(roomId), playerId);
    }

    private void CreateItemsMessage(Guid roomId, List<Item> items, Guid? playerId = null)
    {
        var msg = new ItemsMessage
        {
            Identities = new ulong[items.Count],
            Positions = new Vector2Float[items.Count],
            Radiuses = new float[items.Count],
            Types = new ItemType[items.Count],
            Speeds = new float[items.Count]
        };

        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];

            msg.Identities[i] = item.ItemId;
            msg.Positions[i] = item.Pos;
            msg.Radiuses[i] = item.Radius;
            msg.Types[i] = item.Type;
            msg.Speeds[i] = item.Speed;
        }

        byte[] data = _serializerService.Serialize(msg);

        if (playerId != null)
        {
            AddMessageToSinglePlayer(playerId.Value, MessageType.Items, data);
        }
        else
        {
            AddMessageToAllRoomPlayers(roomId, MessageType.Items, data);
        }
    }

    private void AddMessageToAllRoomPlayers(Guid roomId, MessageType type, byte[] data)
    {
        var players = _playersPooling.GetRoomPlayers(roomId);

        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];

            AddMessageToSinglePlayer(player.PlayerId, type, data);
        }
    }

    private void AddMessageToSinglePlayer(Guid playerId, MessageType type, byte[] data)
    {
        _messageId++;

        var msg = new MessageContract
        {
            Id = _messageId,
            Type = type,
            Data = data
        };

        _messagePooling.AddMessage(playerId, msg);
    }
}