using Game.Core.Zombies;
using Entitas;

namespace Assets.Scripts.Features.Zombies
{
    public class ZombieComponent : IComponent
    {
        public ZombieType type;
        public float radius;
    }
}
