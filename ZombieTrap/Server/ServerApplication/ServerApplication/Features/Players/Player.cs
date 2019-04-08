using Game.Core.Networking.Udp;

using System;
using System.Net;

namespace ServerApplication.Features.Players
{
    public class Player
    {
        public Guid PlayerId;
        public Guid RoomId;

        public IPEndPoint EndPoint;

        public UdpSender Sender;
    }
}
