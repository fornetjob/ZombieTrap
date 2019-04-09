public class RoomFactory : FactoryBase
{
    public void Create(uint number)
    {
        var entity = _context.game.CreateEntity();

        entity.AddRoom(number);

        entity.AddView("RoomView", entity);
    }
}