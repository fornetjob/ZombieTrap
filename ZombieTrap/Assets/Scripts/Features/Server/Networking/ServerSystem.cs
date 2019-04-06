using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Udp;

using Entitas;
using System.Net;

namespace Assets.Scripts.Features.Server.Networking
{
    public class ServerSystem : IExecuteSystem, IContextInitialize
    {
        #region Services

        private SerializerService _serializerService = null;
        private ServerMessageProcessor _messageProcessor = null;

        #endregion

        private IListener
            _listener;

        void IContextInitialize.Initialize(Contexts context)
        {
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
            _messageProcessor.Process(ip, msg);
        }
    }
}