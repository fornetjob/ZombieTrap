using Game.Core;
using Game.Core.Networking;
using Game.Core.Networking.Messages;
using System;

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

    public void CreateMessage(Guid roomId, MessageType type)
    {
        byte[] data;

        switch (type)
        {
            case MessageType.Positions:
                data = _serializerService.Serialize(CreatePositionsMessage(roomId));
                break;
            case MessageType.Items:
                data = _serializerService.Serialize(CreateItemsMessage(roomId));
                break;
            default:
                throw new NotSupportedException(type.ToString());
        }

        var players = _playersPooling.GetRoomPlayers(roomId);

        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];

            AddMessageToPooling(player.PlayerId, type, data);
        }
    }

    public void CreateRoomMessage(Guid roomId, Guid playerId)
    {
        AddMessageToPooling(playerId, MessageType.Room, _serializerService.Serialize(new RoomMessage
        {
            Bound = _roomBoundService.GetRoomBound(),
        }));

        AddMessageToPooling(playerId, MessageType.Items, _serializerService.Serialize(CreateItemsMessage(roomId)));
    }

    private PositionsMessage CreatePositionsMessage(Guid roomId)
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

        return posMsg;
    }

    private void AddMessageToPooling(Guid playerId, MessageType type, byte[] data)
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

    private ItemsMessage CreateItemsMessage(Guid roomId)
    {
        var items = _itemsPooling.Get(roomId);

        var msg = new ItemsMessage
        {
            Identities = new ulong[items.Count],
            Positions = new Vector2Float[items.Count],
            Radiuses = new float[items.Count],
            Types = new ItemType[items.Count]
        };

        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];

            msg.Identities[i] = item.ItemId;
            msg.Positions[i] = item.Pos;
            msg.Radiuses[i] = item.Radius;
            msg.Types[i] = item.Type;
        }

        return msg;
    }
}