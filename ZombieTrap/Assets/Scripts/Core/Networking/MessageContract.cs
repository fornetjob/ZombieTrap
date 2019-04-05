namespace Assets.Scripts.Core.Networking
{
    [ProtoBuf.ProtoContract]
    public class MessageContract
    {
        [ProtoBuf.ProtoMember(1)]
        public ulong Id;
        [ProtoBuf.ProtoMember(2)]
        public MessageType Type;
        [ProtoBuf.ProtoMember(3)]
        public byte[] Data;
    }
}