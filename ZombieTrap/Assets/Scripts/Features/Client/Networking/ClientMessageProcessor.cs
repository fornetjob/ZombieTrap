using Assets.Scripts.Core.Networking;
using Assets.Scripts.Features.Core.Networking.Messages;

namespace Assets.Scripts.Features.Client.Networking
{
    public class ClientMessageProcessor:IDependency, IContextInitialize
    {
        #region Services

        private MessageService _messageService = null;

        #endregion

        #region Fields

        private Contexts
            _context;

        #endregion

        #region IContextInitialize

        void IContextInitialize.Initialize(Contexts context)
        {
            _context = context;
        }

        #endregion

        #region Public methods

        public void Process(MessageContract msg)
        {
            switch (msg.Type)
            {
                case MessageType.Zombies:

                    break;
                case MessageType.Positions:
                    OnPositionMessage(_messageService.ConvertToPositionsMessage(msg));
                    break;
                default:
                    throw new System.NotSupportedException(msg.Type.ToString());
            }
        }

        #endregion

        #region Private methods

        private void OnPositionMessage(PositionsMessage msg)
        {
            for (int i = 0; i < msg.Identities.Length; i++)
            {
                var id = msg.Identities[i];
                var pos = msg.Positions[i];

                var enemy = _context.game.GetEntityWithIdentity(id);

                if (enemy != null)
                {
                    enemy.ReplacePosition(pos);
                }
            }
        }

        #endregion
    }
}
