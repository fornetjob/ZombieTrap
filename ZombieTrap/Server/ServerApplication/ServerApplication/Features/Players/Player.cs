using Assets.Scripts.Core.Networking.Udp;

using System;

namespace ServerApplication.Features.Players
{
    public class Player
    {
        public Guid PlayerId;
        public UdpSender Sender;
    }
}
