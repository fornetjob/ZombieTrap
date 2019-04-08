using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Assets.Scripts.Features.Move
{
    [Event(EventTarget.Self)]
    public class MoveComponent:IComponent
    {
        public Vector3 moveDir;
        public Vector3 posTo;
        public float speed;
    }
}
