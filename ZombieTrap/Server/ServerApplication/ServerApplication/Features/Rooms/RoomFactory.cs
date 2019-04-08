using Game.Core;
using ServerApplication.Features.Rooms;
using System;

public class RoomFactory : IDependency
{
    #region Services

    private SettingsService _settingsService = null;

    #endregion

    #region Factories

    private ItemFactory _itemFactory = null;

    #endregion

    #region Poolings

    private TimeService _timeService = null;
    private RoomsPooling _roomsPooling = null;

    #endregion

    public Room CreateRoom()
    {
        var room = new Room
        {
            RoomId = Guid.NewGuid(),
            MaxScore = 1000,
            MaxZombieCount = 20,
            MaxPlayerCount = 4,
            SpawnTimeEvent = _timeService.CreateTimeEvent(0.3f)
        };

        _roomsPooling.Rooms.Add(room);

#if DEBUG
        System.Console.WriteLine("Created room {0}", room.RoomId);
#endif

        var lampRadius = _settingsService.GetItemRadius(ItemType.Lamp);

        _itemFactory.Create(room.RoomId, ItemType.Lamp, lampRadius, new Vector2Float(-9.5f, 4.5f));
        _itemFactory.Create(room.RoomId, ItemType.Lamp, lampRadius, new Vector2Float(9.5f, 4.5f));

        _itemFactory.Create(room.RoomId, ItemType.Lamp, lampRadius, new Vector2Float(-5.5f, 4.5f));
        _itemFactory.Create(room.RoomId, ItemType.Lamp, lampRadius, new Vector2Float(5.5f, 4.5f));

        _itemFactory.Create(room.RoomId, ItemType.Lamp, lampRadius, new Vector2Float(-1.5f, 4.5f));
        _itemFactory.Create(room.RoomId, ItemType.Lamp, lampRadius, new Vector2Float(1.5f, 4.5f));

        return room;
    }
}