using Game.Core;
using ServerApplication.Features.Items;
using System;

public class ProjectileFactory:IDependency
{
    #region Services

    private TimeService _timeService = null;
    private RoomBoundService _roomBoundService = null;
    private RandomService _randomService = null;
    private SettingsService _settingsService = null;

    #endregion

    #region Poolings

    private ItemIdPooling _itemIdPooling = null;
    private ProjectilePooling _projectilePooling = null;

    #endregion

    public Item Create(Guid roomId, ItemType type)
    {
        if (type.IsProjectile() == false)
        {
            throw new ArgumentOutOfRangeException("type");
        }

        float speed = _settingsService.GetItemSpeed(type);
        var radius = _settingsService.GetItemRadius(type);
        // Возьмём чуть побольше радиус взрыва
        var bound = _roomBoundService.GetRadiusBound(radius * 2);
        var pos = _randomService.RandomPos(bound);
        var explosionTime = _randomService.Range(2, 6) / 2f;

        var item = new Item
        {
            ItemId = _itemIdPooling.NewId(),
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
}
