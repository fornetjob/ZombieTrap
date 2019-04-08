using DG.Tweening;
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
            _tr = gameObject.transform;
            _projectileParticle = _tr.Find("ProjectileParticle").GetComponent<ParticleSystem>();
            _explosionParticle = _tr.Find("ExplosionParticle").GetComponent<ParticleSystem>();

            _tr.position = entity.projectile.posFrom;

            _projectileParticle.Play();
            _tr.DOMove(entity.projectile.posTo, entity.projectile.duration)
                .OnComplete(() =>
                {
                    _projectileParticle.Stop();

                    _explosionParticle.Play();
                });
        }
    }
}