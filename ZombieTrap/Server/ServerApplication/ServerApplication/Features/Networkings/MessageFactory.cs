using Assets.Scripts.Core.Networking.Messages;

using Game.Core;
using Game.Core.Networking;

using System;

namespace ServerApplication.Features.Networkings
{
    public class MessageFactory:IDependency
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

        public MessageContract CreateMessage(Guid playerId, MessageType type)
        {
            byte[] data;

            switch(type)
            {
                case MessageType.Room:
                    data = _serializerService.Serialize(CreateRoomMessage(playerId));
                    break;
                case MessageType.Zombies:
                    data = _serializerService.Serialize(CreateItemsMessage(playerId));
                    break;
                default:
                    throw new System.NotSupportedException(type.ToString());
            }

            _messageId++;

            var msg = new MessageContract
            {
                Id = _messageId,
                Type = type,
                Data = data
            };

            _messagePooling.AddMessage(playerId, msg);

            return msg;
        }

        private RoomMessage CreateRoomMessage(Guid playerId)
        {
            return new RoomMessage
            {
                Bound = _roomBoundService.GetRoomBound(),
                Items = CreateItemsMessage(playerId)
            };
        }

        private ItemsMessage CreateItemsMessage(Guid playerId)
        {
            var player = _playersPooling.GetPlayer(playerId);

            var items = _itemsPooling.Get(player.RoomId);

            var msg = new ItemsMessage
            {
                Ids = new ulong[items.Count],
                Poses = new Vector2Float[items.Count],
                Radiuses = new float[items.Count],
                Types = new ItemType[items.Count]
            };

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];

                msg.Ids[i] = item.ItemId;
                msg.Poses[i] = item.Pos;
                msg.Radiuses[i] = item.Radius;
                msg.Types[i] = item.Type;
            }

            return msg;
        }
    }
}
