using Game.Core;
using Game.Core.Zombies;

namespace Assets.Scripts.Core.Networking.Messages
{
    [ProtoBuf.ProtoContract]
    public class ZombiesMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public ulong[] Ids;
        [ProtoBuf.ProtoMember(2)]
        public ZombieType[] Types;
        [ProtoBuf.ProtoMember(3)]
        public float[] Radiuses;
        [ProtoBuf.ProtoMember(4)]
        public Vector2Float Poses;
    }
}