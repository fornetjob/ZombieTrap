using Entitas;
using System;

namespace Assets.Scripts.Features.Server.Room
{
    [ServerSide]
    public class RoomComponent:IComponent
    {
        public int MaxScore;
        public int PlayerCount;
        public int MaxZombieCount;
        public float SpawnTimeInterval;
    }
}
