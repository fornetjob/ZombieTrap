using Game.Core;
using UnityEngine;

public class ItemFactory : FactoryBase
{
    private PrefabsPooling _prefabsPooling = null;

    public GameEntity Create(ulong id, ItemType type, float radius, float speed, Vector2Float pos)
    {
        var entity = _context.game.CreateEntity();

        entity.AddIdentity(id);
        entity.AddItem(type, radius, speed);
        entity.AddPosition(new Vector3(pos.x, 0, pos.y));

        _prefabsPooling.Create(type.ToString(), entity);

        return entity;
    }
}