using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Features.Projectiles
{
    public class ProjectileView : ViewBase
    {
        private Transform
            _tr;

        private ParticleSystem
            _projectileParticle;
        private ParticleSystem
            _explosionParticle;

        protected override void OnEntityAttach(GameEntity entity)
        {
            if (_tr == null)
            {
                _tr = gameObject.transform;
                _projectileParticle = _tr.Find("ProjectileParticle").GetComponent<ParticleSystem>();
                _explosionParticle = _tr.Find("ExplosionParticle").GetComponent<ParticleSystem>();
            }

            _tr.position = entity.projectile.posFrom;

            StartCoroutine(FlyAndExplosionState(entity));
        }

        private IEnumerator FlyAndExplosionState(GameEntity entity)
        {
            _projectileParticle.Play();

            float duration = 0;
            float durationTo = entity.projectile.duration;

            var beginPos = _tr.position;

            while (duration < durationTo)
            {
                duration += Time.deltaTime;

                _tr.position = Vector3.Lerp(beginPos, entity.projectile.posTo, duration / durationTo);

                yield return 0;
            }

            _projectileParticle.Stop();
            _explosionParticle.Play();

            while (_explosionParticle.isPlaying)
            {
                yield return new WaitForSeconds(0.5f);
            }

            entity.isDestroy = true;
        }
    }
}