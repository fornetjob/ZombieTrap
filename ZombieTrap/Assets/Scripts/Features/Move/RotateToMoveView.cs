using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Features.Move
{
    public class RotateToMoveView : ViewBase, IMoveListener
    {
        private Transform
            _tr;

        protected override void OnEntityAttach(GameEntity entity)
        {
            _tr = gameObject.transform;

            entity.AddMoveListener(this);
        }

        void IMoveListener.OnMove(GameEntity entity, Vector3 moveDir, Vector3 posTo, float speed)
        {
            _tr.DORotateQuaternion(Quaternion.LookRotation(moveDir), 1f);
        }
    }
}
