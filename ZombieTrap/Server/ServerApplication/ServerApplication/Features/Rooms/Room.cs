using System;

namespace ServerApplication.Features.Rooms
{
    public class Room
    {
        public Guid RoomId;

        public int MaxScore;
        public int PlayerCount;
        public int MaxZombieCount;

        public TimeEvent SpawnTimeEvent;
    }
}
