namespace Game.Core.Networking
{
    [ProtoBuf.ProtoContract]
    public struct PortInterval
    {
        [ProtoBuf.ProtoMember(1)]
        public int port;
    }
}
