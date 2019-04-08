using UnityEngine;

namespace Assets.Scripts.Features.Move
{
    public class MoveAnimatorView:ViewBase
    {
        private Transform
            _tr;

        private Animator
            _anim;

        private Vector3?
            _prevPos;

        private void Update()
        {
            if (_prevPos != null)
            {
                var velocity = (_tr.position - _prevPos.Value) / Time.deltaTime;

                _anim.SetFloat("Speed", velocity.magnitude);
            }

            _prevPos = _tr.position;
        }

        protected override void OnEntityAttach(GameEntity entity)
        {
            _tr = gameObject.transform;
            _anim = gameObject.GetComponent<Animator>();
        }
    }
}