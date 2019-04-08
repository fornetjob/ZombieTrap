using System;

namespace Game.Core
{
    [ProtoBuf.ProtoContract]
    public struct RectangleFloat
    {
        public RectangleFloat(Vector2Float center, Vector2Float size)
        {
            this.center = center;
            this.size = size;
        }

        [ProtoBuf.ProtoMember(1)]
        public Vector2Float center;
        [ProtoBuf.ProtoMember(2)]
        public Vector2Float size;

        public Vector2Float min { get { return center - size/2f; }}

        public Vector2Float max { get { return center + size / 2f; }}

        public bool Overlapping(Vector2Float pos)
        {
            bool insideX = center.x - size.x < pos.x && pos.x < center.x + size.x;
            bool insideY = center.y - size.y < pos.y && pos.y < center.y + size.y;

            return insideX && insideY;
        }

        public RectangleFloat Expand(float expandSize)
        {
            return new RectangleFloat(center, new Vector2Float(size.x + expandSize, size.y + expandSize));
        }
    }
}