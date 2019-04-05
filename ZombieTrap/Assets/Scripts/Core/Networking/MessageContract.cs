using System.IO;

namespace Assets.Scripts.Core.Networking
{
    public class MessageContract
    {
        [ProtoBuf.ProtoMember(1)]
        public ulong Id;
        [ProtoBuf.ProtoMember(2)]
        public MessageType Type;
    }
}