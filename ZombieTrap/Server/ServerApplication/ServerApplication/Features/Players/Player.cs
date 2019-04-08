using Game.Core.Networking.Udp;

using System;

namespace ServerApplication.Features.Players
{
    public class Player
    {
        public Guid PlayerId;
        public Guid RoomId;

        public UdpSender Sender;
    }
}
