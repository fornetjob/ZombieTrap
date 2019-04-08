using Assets.Scripts.Core.Networking.Messages;
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

            return null;
        }
    }
}
