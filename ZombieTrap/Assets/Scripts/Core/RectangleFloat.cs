using System;

namespace Assets.Scripts.Core
{
    public struct RectangleFloat
    {
        public RectangleFloat(Vector2Float center, Vector2Float size)
        {
            this.center = center;
            this.size = size;
        }

        public Vector2Float center;
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

        public Vector2Float ClosestPoint(Vector2Float pos)
        {
            if (Overlapping(pos))
            {
                return PointClampedToBox(pos);
            }
            else
            {
                return ClosestInsideOut(pos);
            }
        }

        public Vector2Float PointClampedToBox(Vector2Float pos)
        {
            return new Vector2Float(
                Math.Max(center.x - size.x, Math.Min(pos.x, center.x + size.x)),
                Math.Max(center.y - size.y, Math.Min(pos.y, center.y + size.y)));
        }

        public Vector2Float ClosestInsideOut(Vector2Float _point)
        {
            var deltaToPositiveBounds = center + size - _point;
            var deltaToNegativeBounds = -(center - size - _point);
            var smallestX = Math.Min(deltaToPositiveBounds.x, deltaToNegativeBounds.x);
            var smallestY = Math.Min(deltaToPositiveBounds.y, deltaToNegativeBounds.y);
            var smallestDistance = Math.Min(smallestX, smallestY);

            if (smallestDistance == deltaToPositiveBounds.x)
            {
                return new Vector2Float(center.x + size.x, _point.y);
            }
            else if (smallestDistance == deltaToNegativeBounds.x)
            {
                return new Vector2Float(center.x - size.x, _point.y);
            }
            else if (smallestDistance == deltaToPositiveBounds.y)
            {
                return new Vector2Float(_point.x, center.y + size.y);
            }
            else
            {
                return new Vector2Float(_point.x, center.y - size.y);
            }
        }
    }
}