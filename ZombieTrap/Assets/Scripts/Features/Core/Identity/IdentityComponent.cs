using Entitas;

namespace Assets.Scripts.Features.Core.Identity
{
    [Game, ServerSide]
    public class IdentityComponent:IComponent
    {
        public ulong value;
    }
}