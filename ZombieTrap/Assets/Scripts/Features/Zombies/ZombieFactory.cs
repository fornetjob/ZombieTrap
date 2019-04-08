using Game.Core;
using UnityEngine;

namespace Assets.Scripts.Features.Zombies
{
    public class ZombieFactory:FactoryBase
    {
        public GameEntity Create(ulong id, ItemType type, float radius, Vector3 pos)
        {
            var entity = _context.game.CreateEntity();

            entity.AddIdentity(id);
            entity.AddZombie(type, radius);
            entity.ReplacePosition(pos);

            return entity;
        }
    }
}
