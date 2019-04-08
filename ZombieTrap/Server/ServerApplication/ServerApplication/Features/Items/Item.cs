using Game.Core;
using System;

namespace ServerApplication.Features.Items
{
    public class Item
    {
        public uint ItemId;
        public Guid RoomId;

        public ItemType Type;

        public float Radius;
        public Vector2Float Pos;

        public float Speed;
        public Vector2Float MoveToPos;

        public ItemState State;

        public float WaitTo;

        public int Health;
    }
}