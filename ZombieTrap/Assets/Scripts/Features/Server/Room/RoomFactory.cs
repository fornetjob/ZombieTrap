using Assets.Scripts.Features.Server.Room;
using System;
using UnityEngine;

public class RoomFactory : FactoryBase
{
    public static readonly Bounds RoomBound = new Bounds(Vector3.zero, new Vector3(20, 10, 1));

    public ServerSideEntity Create()
    {
        var entity = _context.serverSide.CreateEntity();

        entity.AddIdentity(0);
        entity.AddRoomIdentity(Guid.NewGuid());
        entity.AddRoom(1000, 2, 28, 0.3f);
        entity.AddBound(RoomBound);

        _context.feautures.Add(new RoomSystem(entity));

        return entity;
    }
}