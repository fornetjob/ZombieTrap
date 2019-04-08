using Game.Core;

namespace Game.Core.Networking.Messages
{
    [ProtoBuf.ProtoContract]
    public class ItemsMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public PositionsMessage PositionsMessage;
        [ProtoBuf.ProtoMember(2)]
        public ItemType[] Types;
        [ProtoBuf.ProtoMember(3)]
        public float[] Radiuses;
    }
}