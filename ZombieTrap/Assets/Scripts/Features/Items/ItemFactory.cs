using Game.Core;
using UnityEngine;

public class ItemFactory : FactoryBase
{
    #region Services

    private GameTimeService _gameTimeService = null;

    #endregion

    private PrefabsPooling _prefabsPooling = null;

    public GameEntity Create(ulong id, ItemType type, float radius, float speed, Vector2Float pos, int health, float waitTo)
    {
        if(type.IsProjectile() == false )
        {
            return CreateItem(id, type, radius, speed, pos, health, waitTo);
        }
        else
        {
            return CreateProjectile(id, type, radius, speed, pos, health, waitTo);
        }
    }

    private GameEntity CreateProjectile(ulong id, ItemType type, float radius, float speed, Vector2Float pos, int health, float waitTo)
    {
        var entity = _context.game.CreateEntity();

        var posTo = new Vector3(pos.x, 0.5f, pos.y);
        var posFrom = new Vector3(pos.x, 10, pos.y);

        entity.AddIdentity(id);
        entity.AddProjectile(waitTo - _gameTimeService.GetGameTime(), posFrom, posTo);

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