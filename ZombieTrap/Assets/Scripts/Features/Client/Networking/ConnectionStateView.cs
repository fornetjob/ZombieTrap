using Game.Core.Networking;
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

        #region Fields

        private readonly WeakDictionary<int, string>
            _dotsDict = new WeakDictionary<int, string>((dotCount) => { return new string('.', dotCount); });

        #endregion

        protected override void OnEntityAttach(GameEntity entity)
        {
            entity.AddConnectionStateListener(this);
        }

        public void OnConnectionState(GameEntity entity, ConnectionState value, int tryCount)
        {
            bool isActive = true;

            string dots = _dotsDict[tryCount % 4];

            switch (value)
            {
                case ConnectionState.Connecting:
                    _text.text = string.Format("{0}{1}{0}", dots, "CONNECTING");
                    _text.color = Color.yellow;
                    break;
                case ConnectionState.Lost:
                    _text.text = string.Format("{0}{1}{0}", dots, "SERVER LOST");
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