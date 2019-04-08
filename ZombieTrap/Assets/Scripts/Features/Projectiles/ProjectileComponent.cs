using Entitas;
using UnityEngine;

namespace Assets.Scripts.Features.Projectiles
{
    public class ProjectileComponent:IComponent
    {
        public float duration;
        public Vector3 posFrom;
        public Vector3 posTo;
    }
}
