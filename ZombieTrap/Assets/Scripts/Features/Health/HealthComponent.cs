using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Assets.Scripts.Features.Health
{
    [Event(EventTarget.Self)]
    public class HealthComponent:IComponent
    {
        public int value;
        public Vector3 hitPos;
    }
}
