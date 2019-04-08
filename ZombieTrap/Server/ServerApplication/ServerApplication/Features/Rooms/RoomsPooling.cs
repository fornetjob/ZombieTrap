using ServerApplication.Features.Rooms;
using System;
using System.Collections.Generic;

public class RoomsPooling : IDependency
{
    private object _lockObj = new object();

    private PlayersPooling _playersPooling = null;

    public List<Room> Rooms = new List<Room>();

    public Room GetNotFullRoom()
    {
        lock(_lockObj)
        {
            for (int i  = 0; i < Rooms.Count; i++)
            {
                var room = Rooms[i];

                if (room.MaxPlayerCount > _playersPooling.GetPlayerCount(room.RoomId))
                {
                    return room;
                }
            }
        }

        return null;
    }

    public Room GetRoom(Guid roomId)
    {
        for (int i = 0; i < Rooms.Count; i++)
        {
            var room = Rooms[i];

            if (room.RoomId == roomId)
            {
                return room;
            }
        }

        return null;
    }
}