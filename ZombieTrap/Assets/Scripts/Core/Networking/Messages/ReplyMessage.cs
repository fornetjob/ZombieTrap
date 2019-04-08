using System;

namespace Game.Core.Networking.Messages
{
    [ProtoBuf.ProtoContract]
    public class ReplyMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public Guid PlayerId;
    }
}
