using Game.Core.Networking;
using Game.Core.Networking.Messages;

using System.Net;

namespace ServerApplication.Features.Networkings
{
    public class ServerMessageProcessor : IDependency
    {
        #region Services

        private MessageService _messageService = null;

        #endregion

        #region Poolings

        private PlayersPooling _playersPooling = null;

        #endregion

        #region Factories

        private PlayerFactory _playerFactory = null;

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
            System.Console.WriteLine(ip.ToString());

            if (_playersPooling.IsExistPlayer(msg.PlayerId) == false)
            {
                _playerFactory.Create(msg.PlayerId, ip);
            }
        }
    }
}
