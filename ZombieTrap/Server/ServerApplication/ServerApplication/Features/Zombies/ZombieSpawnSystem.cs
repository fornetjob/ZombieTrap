using Assets.Scripts.Core;
using Assets.Scripts.Core.Zombies;

using System;

public class ZombieSpawnSystem : IFixedExecuteSystem
{
    #region Services

    private RandomService _randomService = null;
    private SettingsService _settingsService = null;
    private RoomBoundService _roomBoundService = null;

    #endregion

    #region Factories

    private ZombieFactory _zombieFactory = null;

    #endregion

    #region Poolings

    private ZombiesPooling _zombiesPooling = null;
    private RoomsPooling _roomsPooling = null;
    
    #endregion

    public void FixedExecute()
    {
        for (int i = 0; i < _roomsPooling.Rooms.Count; i++)
        {
            var room = _roomsPooling.Rooms[i];

            if (room.SpawnTimeEvent.Check()
                && _zombiesPooling.GetCount(room.RoomId) < room.MaxZombieCount)
            {
                ZombieType type = (ZombieType)_randomService.Range(0, 3);
                float radius = _settingsService.GetZombieRadius(type);

                var spawnBound = _roomBoundService.GetBound(radius);

                var pos = _randomService.RandomPos(spawnBound);

                if (IsZombieIntersect(room.RoomId, pos, radius) == false)
                {
                    _zombieFactory.Create(room.RoomId, type, radius, pos);
                }
            }
        }
    }

    private bool IsZombieIntersect(Guid roomId, Vector2Float pos, float radius)
    {
        var zombies = _zombiesPooling.GetZombies(roomId);

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