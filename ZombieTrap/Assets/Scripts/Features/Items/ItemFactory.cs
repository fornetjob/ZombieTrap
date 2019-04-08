using Game.Core;

public class ItemFactory : FactoryBase
{
    public GameEntity Create(ulong id, ItemType type, float radius)
    {
        var entity = _context.game.CreateEntity();

        entity.AddIdentity(id);
        entity.AddZombie(type, radius);

        return entity;
    }
}