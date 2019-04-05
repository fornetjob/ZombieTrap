using Assets.Scripts.Core.Networking;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Assets.Scripts.Features.Client.Networking
{
    public class UdpMessageListener : IMessageListener
    {
        #region Fields

        private readonly ListenerConfiguration
            _config;

        private UdpClient
            _listener;

        private bool
            _isOpened;

        private MessageFragmenter
            _fragmenter = new MessageFragmenter();

        private Dictionary<int, MessageFragment[]>
            _fragmentDict = new Dictionary<int, MessageFragment[]>();

        #endregion

        public event MessageEventHandler OnReceive;

        public UdpMessageListener(ListenerConfiguration config)
        {
            _config = config;
        }

        public void Open()
        {
            if (_isOpened)
            {
                throw new System.NotSupportedException("Listener already opened");
            }

            _isOpened = true;

            Task.Run(async () =>
            {
                using (var udpClient = new UdpClient(_config.Address, _config.Port))
                {
                    udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _config.ReceiveTimeout);

                    while (_isOpened)
                    {
                        var receivedResults = await udpClient.ReceiveAsync();

                        var fragment = new MessageFragment(receivedResults.Buffer);

                        if (fragment.Index + 1 == fragment.Count)
                        {
                            var message = _fragmenter.Defragment(fragment);

                            var reply = new MessageContract
                            {
                                Id = message.Id,
                                Type = MessageType.Reply
                            };

                            var replyData = _fragmenter.Fragment(reply)[0].Data;

                            udpClient.Send(replyData, replyData.Length, receivedResults.RemoteEndPoint);
                            OnReceive(message);
                        }
                        else
                        {
                            throw new System.NotSupportedException();
                        }
                    }
                }
            });
        }

        public void Close()
        {
            _isOpened = false;
        }
    }
}
