﻿using System;

namespace Assets.Scripts.Features.Core.Networking.Messages
{
    [ProtoBuf.ProtoContract]
    public class ConnectMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public Guid PlayerId;
    }
}
