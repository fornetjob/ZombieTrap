using Assets.Scripts.Core.Networking;
using System;
using System.Collections.Generic;
using System.Net;

namespace Assets.Scripts.Features.Server.Networking
{
    public class PlayerFactory:FactoryBase
    {
        public ServerSideEntity Create(Guid id, IPEndPoint endPoint)
        {
            var playerEntity = _context.serverSide.CreateEntity();

            playerEntity.AddIdentity(0);
            playerEntity.AddPlayer(id, endPoint, new Queue<MessageContract>());

            return playerEntity;
        }
    }
}
