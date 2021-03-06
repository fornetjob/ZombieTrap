﻿namespace Game.Core.Networking.Messages
{
    [ProtoBuf.ProtoContract]
    public class ServerSyncMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public RectangleFloat Bound;
        [ProtoBuf.ProtoMember(2)]
        public float ServerTime;
        [ProtoBuf.ProtoMember(3)]
        public uint RoomNumber;
    }
}
