using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.Networking.Udp
{
    public class UdpConnection : IConnection
    {
        #region Fields

        private readonly ConnectionConfiguration
            _config;

        private UdpClient
            _sendclient;

        private bool
            _isOpened;

        private MessageFragmenter
            _fragmenter = new MessageFragmenter();

        private Dictionary<int, MessageFragment[]>
            _fragmentDict = new Dictionary<int, MessageFragment[]>();

        #endregion

        public event MessageEventHandler OnReceive;

        public UdpConnection(ConnectionConfiguration config)
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

            _sendclient = new UdpClient();
            _sendclient.Connect(_config.RemoteHost, _config.RemotePort);

            Task.Run(async () =>
            {
                using (var listener = new UdpClient(_config.ListeningPort))
                {
                    listener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _config.ReceiveTimeout);

                    while (_isOpened)
                    {
                        var receivedResults = await listener.ReceiveAsync();

                        var fragment = new MessageFragment(receivedResults.Buffer);

                        if (fragment.Index + 1 == fragment.Count)
                        {
                            var message = _fragmenter.Defragment(fragment);

                            var reply = new MessageContract
                            {
                                Id = message.Id,
                                Type = MessageType.Reply
                            };

                            Send(reply);

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

        public void Send(MessageContract msg)
        {
            var fragments = _fragmenter.Fragment(msg);

            for (int i = 0; i < fragments.Length; i++)
            {
                var fragment = fragments[i];

                _sendclient.Send(fragment.Data, fragment.Data.Length, _config.RemoteHost, _config.RemotePort);
            }
        }
    }
}
