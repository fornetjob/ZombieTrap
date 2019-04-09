using UnityEngine;

namespace Assets.Scripts.Features.Move
{
    public class MoveAnimatorView:ViewBase, IMoveListener
    {
        private Transform
            _tr;

        private Animator
            _anim;

        private Vector3?
            _prevPos;

        private float
            _speed;

        private Quaternion?
            _rotationTo;

        private void Update()
        {
            if (_prevPos != null)
            {
                _speed = ((_tr.position - _prevPos.Value) / Time.deltaTime).magnitude;

                _anim.SetFloat("Speed", _speed);

                if (_speed > 0
                    && _rotationTo != null)
                {
                    _tr.rotation = Quaternion.RotateTowards(transform.rotation, _rotationTo.Value, 120 * Time.deltaTime);
                }
            }

            _prevPos = _tr.position;
        }

        protected override void OnEntityAttach(GameEntity entity)
        {
            _tr = gameObject.transform;
            _anim = gameObject.GetComponent<Animator>();

            entity.AddMoveListener(this);
        }

        void IMoveListener.OnMove(GameEntity entity, Vector3 moveDir, Vector3 posTo, float speed)
        {
            _rotationTo = Quaternion.LookRotation(moveDir);
        }
    }
}