using UnityEngine.UI;

namespace Assets.Scripts.Features.Room
{
    public class RoomView:ViewBase, IRoomListener
    {
        private Text
            _text;

        protected override void OnEntityAttach(GameEntity entity)
        {
            _text = gameObject.GetComponent<Text>();

            entity.AddRoomListener(this);
        }

        void IRoomListener.OnRoom(GameEntity entity, uint number)
        {
            _text.text = string.Format("ROOM #{0}", number);

            gameObject.SetActive(true);
        }
    }
}
