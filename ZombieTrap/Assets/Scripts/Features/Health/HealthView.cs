using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Features.Health
{
    [RequireComponent(typeof(Animator))]
    public class HealthView:ViewBase, IHealthListener
    {
        private Animator
            _anim;

        private Transform
            _tr;

        private ParticleSystem
            _hideParticle;

        private SkinnedMeshRenderer
            _mesh;

        protected override void OnEntityAttach(GameEntity entity)
        {
            if (_tr == null)
            {
                _tr = gameObject.transform;
                _anim = _tr.GetComponent<Animator>();
                _hideParticle = _tr.Find("HideParticle").GetComponent<ParticleSystem>();
                _mesh = _tr.GetComponentInChildren<SkinnedMeshRenderer>();
            }

            entity.AddHealthListener(this);
        }

        protected override void OnEntityDettach(GameEntity entity)
        {
            _mesh.enabled = true;
        }

        public void OnHealth(GameEntity entity, int value, Vector3 hitPos)
        {
            if (_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Fall"))
            {
                return;
            }

            var dir = (_tr.position - hitPos).normalized;

            dir[1] = 0;

            _anim.SetTrigger("Fall");

            int fallType = 0;

            if (Vector3.Dot(dir, _tr.forward) < 0)
            {
                dir *= -1;

                fallType = 1;
            }

            _anim.SetInteger("Type", fallType);

            _tr.rotation = Quaternion.LookRotation(dir);

            if (value == 0)
            {
                StartCoroutine(DeadState(entity));
            }
        }

        private IEnumerator DeadState(GameEntity entity)
        {
            yield return new WaitForSeconds(3f);

            _hideParticle.Play();

            yield return new WaitForSeconds(0.8f);

            _mesh.enabled = false;

            while (_hideParticle.isPlaying)
            {
                yield return new WaitForSeconds(0.3f);
            }

            entity.isDestroy = true;
        }
    }
}