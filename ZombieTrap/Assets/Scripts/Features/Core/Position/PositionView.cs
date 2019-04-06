using UnityEngine;

namespace Assets.Scripts.Features.Core.Position
{
    public class PositionView:ViewBase, IPositionListener
    {
        private Transform
            _tr;

        protected override void OnEntityAttach(GameEntity entity)
        {
            _tr = gameObject.transform;

            entity.AddPositionListener(this);

            OnPosition(entity, entity.position.value);
        }

        public void OnPosition(GameEntity entity, Vector3 value)
        {
            _tr.position = value;
        }
    }
}
