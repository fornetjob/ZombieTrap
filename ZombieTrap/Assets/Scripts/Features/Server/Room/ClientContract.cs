using Assets.Scripts.Core.Networking;
using System;
using System.Collections.Generic;
using System.Net;

namespace Assets.Scripts.Features.Server.Room
{
    public class ClientContract
    {
        public ulong Id;

        public Guid PlayerId;
        public IPEndPoint EndPoint;

        public Queue<MessageContract> StrongMessagesQueue = new Queue<MessageContract>();
    }
}
