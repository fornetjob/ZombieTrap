using Game.Core;

namespace Game.Core.Networking.Messages
{
    [ProtoBuf.ProtoContract]
    public class ItemsMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public ulong[] Identities;
        [ProtoBuf.ProtoMember(2)]
        public Vector2Float[] Positions;
        [ProtoBuf.ProtoMember(3)]
        public ItemType[] Types;
        [ProtoBuf.ProtoMember(4)]
        public float[] Radiuses;
        [ProtoBuf.ProtoMember(5)]
        public float[] Speeds;
        [ProtoBuf.ProtoMember(6)]
        public int[] Healths;
        [ProtoBuf.ProtoMember(7)]
        public float[] WaitTo;
    }
}