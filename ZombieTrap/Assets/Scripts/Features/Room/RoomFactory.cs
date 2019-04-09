public class RoomFactory : FactoryBase
{
    public void Create(uint number)
    {
        var entity = _context.game.SetRoom(number);

        entity.AddView("RoomView", entity);
    }
}