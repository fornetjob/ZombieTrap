using System;
using UnityEngine;

namespace Assets.Scripts.Features.Server.Room
{
    public class RoomFactory:FactoryBase
    {
        public ServerSideEntity Create(Bounds bound)
        {
            var entity = _context.serverSide.CreateEntity();
            
            entity.AddIdentity(0);
            entity.AddRoomIdentity(Guid.NewGuid());
            entity.AddRoom(1000, 2, 28, 0.3f);
            entity.AddBound(bound);

            _context.feautures.Add(new RoomSystem(entity));

            return entity;
        }
    }
}
