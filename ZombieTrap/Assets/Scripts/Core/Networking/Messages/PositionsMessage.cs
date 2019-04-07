using UnityEngine;

namespace Assets.Scripts.Core.Networking.Messages
{
    [ProtoBuf.ProtoContract]
    public class PositionsMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public ulong[] Identities;
        [ProtoBuf.ProtoMember(2)]
        public Vector2[] Positions;
    }
}
