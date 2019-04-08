using Game.Core;

using System;

public class ZombieSpawnSystem : IFixedExecuteSystem
{
    #region Services

    private RandomService _randomService = null;
    private SettingsService _settingsService = null;
    private RoomBoundService _roomBoundService = null;

    #endregion

    #region Factories

    private ItemFactory _itemFactory = null;

    #endregion

    #region Poolings

    private ItemsPooling _itemsPooling = null;
    private RoomsPooling _roomsPooling = null;
    
    #endregion

    public void FixedExecute()
    {
        for (int i = 0; i < _roomsPooling.Rooms.Count; i++)
        {
            var room = _roomsPooling.Rooms[i];

            if (room.SpawnTimeEvent.Check()
                && _itemsPooling.GetZombies(room.RoomId).Count < room.MaxZombieCount)
            {
                ItemType type = _randomService.GetRandomZombie();

                float radius = _settingsService.GetItemRadius(type);
                var spawnBound = _roomBoundService.GetRadiusBound(radius);

                var pos = _randomService.RandomPos(spawnBound);

                if (IsZombieIntersect(room.RoomId, pos, radius) == false)
                {
                    _itemFactory.Create(room.RoomId, type, radius, pos);
                }
            }
        }
    }

    private bool IsZombieIntersect(Guid roomId, Vector2Float pos, float radius)
    {
        var zombies = _itemsPooling.Get(roomId);

        for (int i = 0; i < zombies.Count; i++)
        {
            var zombie = zombies[i];

            var distBetweenZombies = Vector2Float.Distance(pos, zombie.Pos);

            if (distBetweenZombies < radius + zombie.Radius)
            {
                return true;
            }
        }

        return false;
    }
}