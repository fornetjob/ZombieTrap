using System.IO;

namespace Assets.Scripts.Core.Networking
{
    [ProtoBuf.ProtoContract]
    public class MessageContract
    {
        public static readonly MessageContract ConnectMessage = new MessageContract { Type = MessageType.Connect };

        [ProtoBuf.ProtoMember(1)]
        public ulong Id;
        [ProtoBuf.ProtoMember(2)]
        public MessageType Type;
    }
}