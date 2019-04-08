using Game.Core.Networking;
using Game.Core.Networking.Messages;
using ServerApplication.Features.Players;
using ServerApplication.Features.Rooms;
using System.Net;

namespace ServerApplication.Features.Networkings
{
    public class ServerMessageProcessor : IDependency
    {
        #region Services

        private MessageService _messageService = null;

        #endregion

        #region Poolings

        private RoomsPooling _roomsPooling = null;
        private PlayersPooling _playersPooling = null;
        private MessagePooling _messagePooling = null;

        #endregion

        #region Factories

        private MessageFactory _messageFactory = null;
        private PlayerFactory _playerFactory = null;
        private RoomFactory _roomFactory = null;

        #endregion

        public void Process(IPEndPoint ip, MessageContract msg)
        {
            switch (msg.Type)
            {
                case MessageType.Connect:
                    OnConnectMessage(ip, _messageService.ConvertToConnectMessage(msg));
                    break;
                default:
                    throw new System.NotSupportedException(msg.Type.ToString());
            }
        }

        private void OnConnectMessage(IPEndPoint ip, ConnectMessage msg)
        {
            if (_playersPooling.IsExistPlayer(msg.PlayerId) == false)
            {
                var room = _roomsPooling.GetNotFullRoom();

                if (room == null)
                {
                    room = _roomFactory.CreateRoom();
                }

                _playerFactory.Create(room.RoomId, msg.PlayerId, ip);
            }

            // Очистим очередь сообщений игрока
            _messagePooling.Clear(msg.PlayerId);

            _messageFactory.CreateMessage(msg.PlayerId, MessageType.Room);
        }
    }
}
