using System;
using System.Net.Sockets;

namespace Assets.Scripts.Core.Networking.Udp
{
    public class UdpSender:ISender, IDisposable
    {
        private SendConfiguration
            _configuration;

        private UdpClient
            _sendclient;

        private SerializerService
            _serializerService = new SerializerService();

        public UdpSender( SendConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Open()
        {
            if (_sendclient != null)
            {
                throw new System.NotSupportedException("Sender already opened");
            }

            _sendclient = new UdpClient();

            if (_configuration.EndPoint != null)
            {
                _sendclient.Connect(_configuration.EndPoint);
            }
            else
            {
                _sendclient.Connect(_configuration.RemoteHost, _configuration.RemotePort);
            }
        }

        public void Close()
        {
            _sendclient.Close();
            _sendclient = null;
        }

        public void Send(MessageContract msg)
        {
            var fragments = _serializerService.Fragment(msg);

            for (int i = 0; i < fragments.Length; i++)
            {
                var fragment = fragments[i];

                var data = _serializerService.Serialize(fragment);

                _sendclient.Send(data, data.Length);
            }
        }

        public void Dispose()
        {
            if (_sendclient != null)
            {
                Close();
            }
        }
    }
}
