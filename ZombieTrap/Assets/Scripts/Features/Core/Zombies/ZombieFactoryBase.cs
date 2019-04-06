using UnityEngine;

namespace Assets.Scripts.Features.Core.Zombies
{
    public abstract class ZombieFactoryBase:FactoryBase
    {
        public GameEntity Create(ulong id, ZombieType type, float radius, Vector3 pos)
        {
            var entity = _context.game.CreateEntity();

            entity.AddIdentity(id);
            entity.AddZombie(type, radius);
            entity.ReplacePosition(pos);

            OnCreate(entity);

            return entity;
        }

        protected abstract void OnCreate(GameEntity entity);
    }
}
