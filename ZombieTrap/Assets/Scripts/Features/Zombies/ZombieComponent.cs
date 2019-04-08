using Game.Core;
using Entitas;

namespace Assets.Scripts.Features.Zombies
{
    public class ZombieComponent : IComponent
    {
        public ItemType type;
        public float radius;
    }
}
