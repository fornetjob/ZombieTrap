using Game.Core;

public class ProjectileSpawnerSystem : IFixedExecuteSystem, IDependencyInitialize
{
    #region Services

    private TimeService _timeService = null;

    #endregion

    #region Factories

    private ProjectileFactory _projectileFactory = null;

    #endregion

    #region Poolings

    private RoomsPooling _roomsPooling = null;

    #endregion

    #region Fields

    private TimeEvent _spawnTimeEvent;

    #endregion

    #region IDependencyInitialize

    void IDependencyInitialize.Initialize()
    {
        _spawnTimeEvent = _timeService.CreateTimeEvent(3.5f);
    }

    #endregion

    public void FixedExecute()
    {
        if (_spawnTimeEvent.Check())
        {
            for (int roomIndex = 0; roomIndex < _roomsPooling.Rooms.Count; roomIndex++)
            {
                var room = _roomsPooling.Rooms[roomIndex];

                _projectileFactory.Create(room.RoomId, ItemType.FireProjectile);
            }
        }
    }
}