using Game.Core.Networking;
using Game.Core.Networking.Messages;
using UnityEngine;

namespace Assets.Scripts.Features.Networking
{
    public class ClientMessageProcessor:IDependency, IContextInitialize
    {
        #region Services

        private MessageService _messageService = null;

        #endregion

        #region Factories

        private ItemFactory _itemFactory = null;

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
                case MessageType.Room:
                    OnRoomMessage(_messageService.ConvertToRoomMessage(msg));
                    break;
                case MessageType.Items:
                    OnItemsMessage(_messageService.ConvertToItemsMessage(msg));
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

        private void OnRoomMessage(RoomMessage msg)
        {
        }

        private void OnItemsMessage(ItemsMessage msg)
        {
            for (int i = 0; i < msg.Identities.Length; i++)
            {
                var id = msg.Identities[i];

                var item = _context.game.GetEntityWithIdentity(id);

                if (item == null)
                {
                    item = _itemFactory.Create(id, msg.Types[i], msg.Radiuses[i], msg.Positions[i]);
                }
            }
        }

        private void OnPositionMessage(PositionsMessage msg)
        {
            for (int i = 0; i < msg.Identities.Length; i++)
            {
                var id = msg.Identities[i];
                var pos = msg.Positions[i];

                var enemy = _context.game.GetEntityWithIdentity(id);

                if (enemy != null)
                {
                    enemy.ReplacePosition(new Vector3(pos.x, 0, pos.y));
                }
            }
        }

        #endregion
    }
}
