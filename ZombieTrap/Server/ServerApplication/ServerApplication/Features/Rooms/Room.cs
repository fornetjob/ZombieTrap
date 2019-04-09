using System;

namespace ServerApplication.Features.Rooms
{
    public class Room
    {
        public uint Number;

        public Guid RoomId;

        public int MaxPlayerCount;
        public int MaxZombieCount;

        public TimeEvent SpawnTimeEvent;
    }
}
