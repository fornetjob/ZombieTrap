using Game.Core;
using Game.Core.Zombies;
using System;

namespace ServerApplication.Features.Zombies
{
    public class Zombie
    {
        public uint ZombieId;
        public Guid RoomId;

        public ZombieType Type;

        public float Radius;
        public Vector2Float Pos;

        public float Speed;
        public Vector2Float MoveToPos;

        public ZombieState State;

        public float WaitTo;
    }
}