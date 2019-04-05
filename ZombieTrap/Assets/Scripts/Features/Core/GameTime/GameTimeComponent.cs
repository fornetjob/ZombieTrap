using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Assets.Scripts.Features.Core.GameTime
{
    [Unique]
    public class GameTimeComponent:IComponent
    {
        public float value;
    }
}
