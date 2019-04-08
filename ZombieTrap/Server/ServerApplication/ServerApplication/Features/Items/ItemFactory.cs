using Game.Core;
using ServerApplication.Features.Items;
using System;

public class ItemFactory : IDependency
{
    #region Services

    private TimeService _timeService = null;
    private RoomBoundService _roomBoundService = null;
    private RandomService _randomService = null;
    private SettingsService _settingsService = null;

    #endregion

    #region Poolings

    private ItemsPooling _itemsPooling = null;
    private ProjectilePooling _projectilePooling = null;

    #endregion

    #region Fields

    private uint _itemId = 0;

    #endregion

    public Item CreateProjectile(Guid roomId, ItemType type)
    {
        if (type.IsProjectile() == false)
        {
            throw new ArgumentOutOfRangeException("type");
        }

        _itemId++;

        float speed = _settingsService.GetItemSpeed(type);
        var radius = _settingsService.GetItemRadius(type);
        // Возьмём чуть побольше радиус взрыва
        var bound = _roomBoundService.GetRadiusBound(radius * 2);
        var pos = _randomService.RandomPos(bound);
        var explosionTime = _timeService.GetGameTime() + _randomService.Range(1f, 3f);

        var item = new Item
        {
            ItemId = _itemId,
            RoomId = roomId,
            Type = type,
            Radius = radius,
            Speed = speed,
            Pos = pos,
            State = ItemState.Wait,
            WaitTo = explosionTime,
            // Damage
            Health = _settingsService.GetItemHealth(type)
        };

        _projectilePooling.Add(item);

#if DEBUG
        System.Console.WriteLine("\tCreated {0}\t{1}", item.ItemId, item.Type);
#endif

        return item;
    }

    public Item Create(Guid roomId, ItemType type, float radius, Vector2Float pos)
    {
        _itemId++;

        float speed = _settingsService.GetItemSpeed(type);

        int health = 0;

        if (type.IsDamagable())
        {
            health = _settingsService.GetItemHealth(type);
        }

        var item = new Item
        {
            ItemId = _itemId,
            RoomId = roomId,
            Type = type,
            Radius = radius,
            Speed = speed,
            Pos = pos,
            State = ItemState.Wait,
            Health = health
        };

        _itemsPooling.Add(item);

#if DEBUG
        System.Console.WriteLine("\tCreated {0}\t{1}", item.ItemId, item.Type);
#endif

        return item;
    }
}
