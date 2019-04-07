public class RoomSystem : IFixedExecuteSystem, IDependencyInitialize
{
    #region Factories

    private RoomFactory _roomFactory = null;

    #endregion

    public void Initialize()
    {
        _roomFactory.CreateRoom();
    }

    public void FixedExecute()
    {
        
    }
}