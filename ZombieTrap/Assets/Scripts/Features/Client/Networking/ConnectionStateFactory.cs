
using Assets.Scripts.Core.Networking;

namespace Assets.Scripts.Features.Client.Networking
{
    public class ConnectionStateFactory
    {
        private Contexts
            _context;

        public ConnectionStateFactory(Contexts context)
        {
            _context = context;
        }

        public GameEntity Create()
        {
            var entity = _context.game.CreateEntity();
            entity.AddConnectionState(ConnectionState.Connecting);

            entity.AddView("ConnectionView", entity);

            return entity;
        }
    }
}
