using System.Net.Sockets;

namespace Assets.Scripts.Core.Networking.Udp
{
    public class UdpSender:ISender
    {
        private SendConfiguration
            _configuration;

        private UdpClient
            _sendclient;

        private MessageFragmenter
            _fragmenter = new MessageFragmenter();

        public UdpSender(SendConfiguration configuration)
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
        }

        public void Send(MessageContract msg)
        {
            var fragments = _fragmenter.Fragment(msg);

            for (int i = 0; i < fragments.Length; i++)
            {
                var fragment = fragments[i];

                _sendclient.Send(fragment.Data, fragment.Data.Length);
            }
        }
    }
}
