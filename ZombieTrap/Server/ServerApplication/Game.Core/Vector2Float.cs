using System;

namespace Game.Core
{
    public struct Vector2Float
    {
        public Vector2Float(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public const float kEpsilon = 1E-05f;
        public float x;
        public float y;

        public Vector2Float normalized
        {
            get
            {
                Vector2Float vector2 = new Vector2Float(this.x, this.y);
                vector2.Normalize();
                return vector2;
            }
        }

        public float magnitude
        {
            get
            {
                return (float)Math.Sqrt((float)((double)this.x * (double)this.x + (double)this.y * (double)this.y));
            }
        }

        public static Vector2Float zero
        {
            get
            {
                return new Vector2Float(0.0f, 0.0f);
            }
        }

        public static Vector2Float operator *(Vector2Float a, float d)
        {
            return new Vector2Float(a.x * d, a.y * d);
        }

        public static Vector2Float operator +(Vector2Float a, Vector2Float b)
        {
            return new Vector2Float(a.x + b.x, a.y + b.y);
        }

        public static Vector2Float operator -(Vector2Float a, Vector2Float b)
        {
            return new Vector2Float(a.x - b.x, a.y - b.y);
        }

        public static Vector2Float operator -(Vector2Float a)
        {
            return new Vector2Float(-a.x, -a.y);
        }

        public static Vector2Float operator /(Vector2Float a, float d)
        {
            return new Vector2Float(a.x / d, a.y / d);
        }

        public static bool operator ==(Vector2Float lhs, Vector2Float rhs)
        {
            return (double)Vector2Float.SqrMagnitude(lhs - rhs) < 9.99999943962493E-11;
        }

        public static bool operator !=(Vector2Float lhs, Vector2Float rhs)
        {
            return (double)Vector2Float.SqrMagnitude(lhs - rhs) >= 9.99999943962493E-11;
        }

        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Distance(Vector2Float a, Vector2Float b)
        {
            return (a - b).magnitude;
        }


        public static Vector2Float MoveTowards(Vector2Float current, Vector2Float target, float maxDistanceDelta)
        {
            Vector2Float vector2 = target - current;
            float magnitude = vector2.magnitude;
            if ((double)magnitude <= (double)maxDistanceDelta || (double)magnitude == 0.0)
                return target;
            return current + vector2 / magnitude * maxDistanceDelta;
        }

        /// <summary>
        ///   <para>Makes this vector have a magnitude of 1.</para>
        /// </summary>
        public void Normalize()
        {
            float magnitude = this.magnitude;
            if ((double)magnitude > 9.99999974737875E-06)
                this = this / magnitude;
            else
                this = Vector2Float.zero;
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector2Float))
                return false;
            Vector2Float vector2 = (Vector2Float)other;
            if (this.x.Equals(vector2.x))
                return this.y.Equals(vector2.y);
            return false;
        }

        public static float SqrMagnitude(Vector2Float a)
        {
            return (float)((double)a.x * (double)a.x + (double)a.y * (double)a.y);
        }

        public float SqrMagnitude()
        {
            return (float)((double)this.x * (double)this.x + (double)this.y * (double)this.y);
        }
    }
}
