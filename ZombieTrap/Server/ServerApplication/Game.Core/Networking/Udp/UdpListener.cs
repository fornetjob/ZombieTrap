using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Core.Networking.Udp
{
    public class UdpListener : IListener, IDisposable
    {
        #region Fields

        private readonly ListenConfiguration
            _config;

        private bool
            _isOpened;

        private SerializerService
            _serializerService = new SerializerService();

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
                throw new NotSupportedException("Listener already opened");
            }

            _isOpened = true;

            Task.Run(() =>
            {
                using (var listener = new UdpClient(_config.ListeningPort))
                {
                    listener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _config.ReceiveInterval);

                    while (_isOpened)
                    {
                        if (listener.Available > 0)
                        {
                            IPEndPoint ip = null;
                            byte[] data = listener.Receive(ref ip);

                            var fragment = _serializerService.Deserialize<MessageFragment>(data);

                            if (fragment.Index + 1 == fragment.Count)
                            {
                                var message = _serializerService.Defragment(fragment);

                                if (message.Type.IsStrongMessage())
                                {
                                    var reply = new MessageContract
                                    {
                                        Id = message.Id,
                                        Type = MessageType.Reply
                                    };

                                    var replyData = _serializerService.Fragment(reply)[0].Data;

                                    listener.Send(replyData, replyData.Length, ip);
                                }

                                OnReceive(ip, message);
                            }
                            else
                            {
                                throw new NotSupportedException();
                            }
                        }

                        Thread.Sleep(_config.ReceiveInterval);
                    }
                }
            });
        }

        public void Close()
        {
            _isOpened = false;
        }

        public void Dispose()
        {
            if (_isOpened)
            {
                Close();
            }
        }
    }
}
