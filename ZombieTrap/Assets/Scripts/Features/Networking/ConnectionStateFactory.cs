using Game.Core.Networking;

namespace Assets.Scripts.Features.Networking
{
    public class ConnectionStateFactory: FactoryBase
    {
        public GameEntity Create()
        {
            var entity = _context.game.CreateEntity();
            entity.AddConnectionState(ConnectionState.Connecting, 0);

            entity.AddView("ConnectionView", entity);

            return entity;
        }
    }
}
