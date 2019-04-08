using Game.Core;
using Entitas;

namespace Assets.Scripts.Features.Zombies
{
    public class ItemComponent : IComponent
    {
        public ItemType type;
        public float radius;
    }
}
