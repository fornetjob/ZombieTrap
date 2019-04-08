using Entitas;
using UnityEngine;

namespace Assets.Scripts.Features.Move
{
    public class MoveComponent:IComponent
    {
        public Vector3 moveDir;
        public Vector3 posTo;
        public float speed;
    }
}
