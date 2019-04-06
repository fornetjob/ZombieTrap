using Assets.Scripts.Core.Networking;
using Assets.Scripts.Features.Core.Networking.Messages;
using System.Net;

namespace Assets.Scripts.Features.Server.Networking
{
    public class ServerMessageProcessor:IDependency, IContextInitialize
    {
        #region Services

        private MessageService _messageService = null;

        #endregion

        #region Factories

        private PlayerFactory _playerFactory = null;

        #endregion

        #region Fields

        private Contexts _context;

        #endregion

        #region IContextInitialize

        void IContextInitialize.Initialize(Contexts context)
        {
            _context = context;
        }

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
            var player = _context.serverSide.GetEntityWithPlayer(msg.PlayerId);

            if (player == null)
            {
                _playerFactory.Create(msg.PlayerId, ip);
            }
        }
    }
}
