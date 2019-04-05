using Assets.Scripts.Core.Networking;

using TMPro;
using UnityEngine;

namespace Assets.Scripts.Features.Client.Networking
{
    public class ConnectionStateView : ViewBase, IConnectionStateListener
    {
        #region Bindings

        [SerializeField]
        private TextMeshProUGUI
            _text = null;

        #endregion

        protected override void OnEntityAttach(GameEntity entity)
        {
            entity.AddConnectionStateListener(this);

            OnConnectionState(entity, entity.connectionState.value);
        }

        public void OnConnectionState(GameEntity entity, ConnectionState value)
        {
            bool isActive = true;

            switch (value)
            {
                case ConnectionState.Connecting:
                    _text.text = "CONNECTING";
                    _text.color = Color.yellow;
                    break;
                case ConnectionState.Lost:
                    _text.text = "SERVER LOST";
                    _text.color = Color.red;
                    break;
                default:
                    isActive = false;
                    break;
            }

            if (gameObject.activeSelf != isActive)
            {
                gameObject.SetActive(isActive);
            }
        }
    }
}