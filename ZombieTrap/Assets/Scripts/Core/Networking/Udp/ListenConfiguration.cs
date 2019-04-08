using System;

namespace Game.Core.Networking.Udp
{
    [Serializable]
    public class ListenConfiguration
    {
        public int ListeningPort;
        public int ReceiveInterval;
    }
}
