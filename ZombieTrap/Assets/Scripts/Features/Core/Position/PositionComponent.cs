using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Assets.Scripts.Features.Core.Position
{
    [Event(EventTarget.Self)]
    public class PositionComponent:IComponent
    {
        public Vector3 value;
    }
}
