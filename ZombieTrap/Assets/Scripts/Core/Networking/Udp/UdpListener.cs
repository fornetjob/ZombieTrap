using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.Networking.Udp
{
    public class UdpListener : IListener
    {
        #region Fields

        private readonly ListenConfiguration
            _config;

        private bool
            _isOpened;

        private MessageFragmenter
            _fragmenter = new MessageFragmenter();

        private Dictionary<int, MessageFragment[]>
            _fragmentDict = new Dictionary<int, MessageFragment[]>();

        #endregion

        public event MessageEventHandler OnReceive;

        public UdpListener(ListenConfiguration config)
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

                            if (message.Type == MessageType.Message)
                            {
                                var reply = new MessageContract
                                {
                                    Id = message.Id,
                                    Type = MessageType.Reply
                                };

                                var data = _fragmenter.Fragment(reply)[0].Data;

                                listener.Send(data, data.Length, receivedResults.RemoteEndPoint);
                            }

                            OnReceive(receivedResults.RemoteEndPoint, message);
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
