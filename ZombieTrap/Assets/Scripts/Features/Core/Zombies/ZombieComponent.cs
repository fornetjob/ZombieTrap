using Assets.Scripts.Core.Zombies;
using Entitas;

namespace Assets.Scripts.Features.Core.Zombies
{
    public class ZombieComponent : IComponent
    {
        public ZombieType type;
        public float radius;
    }
}
