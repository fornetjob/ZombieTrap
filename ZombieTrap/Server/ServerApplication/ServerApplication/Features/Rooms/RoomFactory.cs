﻿using ServerApplication.Features.Rooms;
using System;

public class RoomFactory : IDependency
{
    #region Poolings

    private TimeService _timeService = null;
    private RoomsPooling _roomsPooling = null;

    #endregion

    public Room CreateRoom()
    {
        var room = new Room
        {
            RoomId = Guid.NewGuid(),
            MaxScore = 1000,
            MaxZombieCount = 20,
            PlayerCount = 4,
            SpawnTimeEvent = _timeService.CreateTimeEvent(0.3f)
        };

        _roomsPooling.Rooms.Add(room);

#if DEBUG
        System.Console.WriteLine("Created room {0}", room.RoomId);
#endif

        return room;
    }
}