using Game.Core.Networking;
using Game.Core.Networking.Messages;
using ServerApplication.Features.Players;
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
                case MessageType.Reply:

                    var reply = _messageService.ConvertToReplyMessage(msg);

                    var strongQueue = _messagePooling.GetStrongMessageQueue(reply.PlayerId);

                    if (strongQueue.Count > 0
                        && strongQueue.Peek().Id == msg.Id)
                    {
                        strongQueue.Dequeue();
                    }

                    break;
                default:
                    throw new System.NotSupportedException(msg.Type.ToString());
            }
        }

        #region Private methods

        private void OnConnectMessage(IPEndPoint ip, ConnectMessage msg)
        {
            var playerEndPoint = new IPEndPoint(ip.Address, msg.Port.port);

            Player player = _playersPooling.GetPlayer(playerEndPoint);

            if (player != null)
            {
                _messagePooling.Clear(player.PlayerId);
                _playersPooling.RemovePlayer(player);
            }

            var room = _roomsPooling.GetNotFullRoom();

            if (room == null)
            {
                room = _roomFactory.CreateRoom();
            }

            player = _playerFactory.Create(room.RoomId, msg.PlayerId, playerEndPoint);

            _messageFactory.CreateRoomMessage(player.RoomId, player.PlayerId);
        }

        #endregion
    }
}
