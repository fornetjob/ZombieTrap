﻿using System;

namespace Game.Core.Networking.Messages
{
    [ProtoBuf.ProtoContract]
    public class ConnectMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public Guid PlayerId;
    }
}
