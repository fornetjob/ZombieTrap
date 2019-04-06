using Assets.Scripts.Core.Networking;

using Entitas;
using Entitas.CodeGeneration.Attributes;

using System;
using System.Collections.Generic;
using System.Net;

namespace Assets.Scripts.Features.Server.Networking
{
    [ServerSide]
    [Event(EventTarget.Self)]
    public class PlayerComponent:IComponent
    {
        [PrimaryEntityIndex]
        public Guid PlayerId;
        public IPEndPoint EndPoint;

        public Queue<MessageContract> StrongMessagesQueue;
    }
}