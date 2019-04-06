using Entitas;
using UnityEngine;

namespace Assets.Scripts.Features.Core.Bound
{
    [Game, ServerSide]
    public class BoundComponent:IComponent
    {
        public Bounds value;
    }
}
