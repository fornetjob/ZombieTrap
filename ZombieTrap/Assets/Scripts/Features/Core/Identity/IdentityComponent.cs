using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Assets.Scripts.Features.Core.Identity
{
    [Game, ServerSide]
    public class IdentityComponent:IComponent
    {
        [PrimaryEntityIndex]
        public ulong value;
    }
}