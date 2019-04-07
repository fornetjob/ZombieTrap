namespace Game.Core.Networking
{
    [ProtoBuf.ProtoContract]
    public class MessageFragment
    {
        public const int FragmentHeaderSize = 4;

        [ProtoBuf.ProtoMember(1)]
        public ushort Index;
        [ProtoBuf.ProtoMember(2)]
        public ushort Count;
        [ProtoBuf.ProtoMember(3)]
        public byte[] Data;
    }
}
