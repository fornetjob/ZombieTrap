using Entitas;
using System;

namespace Assets.Scripts.Features.Server.Room
{
    [ServerSide]
    public class RoomIdentity:IComponent
    {
        public Guid RoomId;
    }
}
