using Game.Core;

namespace Assets.Scripts.Core.Networking.Messages
{
    [ProtoBuf.ProtoContract]
    public class RoomMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public RectangleFloat Bound;
        [ProtoBuf.ProtoMember(2)]
        public ItemsMessage Items;
    }
}
