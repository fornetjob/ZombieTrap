using Game.Core;
using UnityEngine;

public class ItemFactory : FactoryBase
{
    private GameTimeService _timeService = null;
    private RandomService _randomService = null;

    private PrefabsPooling _prefabsPooling = null;

    public GameEntity Create(ulong id, ItemType type, float radius, float speed, Vector2Float pos, int health, float waitTo)
    {
        if(type.IsProjectile() == false )
        {
            return CreateItem(id, type, radius, speed, pos, health, waitTo);
        }
        else
        {
            return CreateProjectile(id, type, pos, waitTo);
        }
    }

    private GameEntity CreateProjectile(ulong id, ItemType type, Vector2Float pos, float waitTime)
    {
        var entity = _context.game.CreateEntity();

        var posTo = new Vector3(pos.x, 0.5f, pos.y);
        var posFrom = new Vector3(pos.x + _randomService.Range(-5, +5), 10, pos.y + _randomService.Range(-5, +5));

        const float explosionParticleDelay = 0.45f;

        entity.AddIdentity(id);
        entity.AddProjectile(waitTime - _timeService.GetGameTime() - explosionParticleDelay, posFrom, posTo);

        _prefabsPooling.Create(type.ToString(), entity);

        return entity;
    }

    private GameEntity CreateItem(ulong id, ItemType type, float radius, float speed, Vector2Float pos, int health, float waitTo)
    {
        var entity = _context.game.CreateEntity();

        entity.AddIdentity(id);
        entity.AddItem(type, radius, speed, waitTo);
        entity.AddPosition(new Vector3(pos.x, 0, pos.y));

        _prefabsPooling.Create(type.ToString(), entity);

        return entity;
    }
}