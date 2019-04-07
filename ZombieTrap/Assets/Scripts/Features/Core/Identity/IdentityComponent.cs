using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Assets.Scripts.Features.Core.Identity
{
    [Game]
    public class IdentityComponent:IComponent
    {
        [PrimaryEntityIndex]
        public ulong value;
    }
}