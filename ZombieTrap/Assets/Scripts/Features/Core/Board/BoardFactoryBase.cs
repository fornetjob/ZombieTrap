using UnityEngine;

public abstract class BoardFactoryBase : FactoryBase
{
    public GameEntity Create(Bounds bound)
    {
        var entity = _context.game.CreateEntity();

        entity.isBoard = true;
        entity.AddBound(bound);

        OnCreate(entity);

        return entity;
    }

    protected abstract void OnCreate(GameEntity entity);
}