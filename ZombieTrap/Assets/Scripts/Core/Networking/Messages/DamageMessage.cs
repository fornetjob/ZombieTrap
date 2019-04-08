﻿namespace Game.Core.Networking.Messages
{
    [ProtoBuf.ProtoContract]
    public class DamageMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public ulong[] Identities;
        [ProtoBuf.ProtoMember(2)]
        public int[] Healths;
    }
}