using Game.Core;
using ServerApplication.Features.Items;
using System;

public class ItemFactory : IDependency
{
    #region Services

    private TimeService _timeService = null;
    private SettingsService _settingsService = null;

    #endregion

    #region Poolings

    private ItemIdPooling _itemIdPooling = null;
    private ItemsPooling _itemsPooling = null;

    #endregion

    public Item Create(Guid roomId, ItemType type, float radius, Vector2Float pos)
    {
        float speed = _settingsService.GetItemSpeed(type);

        int health = 0;

        if (type.IsDamagable())
        {
            health = _settingsService.GetItemHealth(type);
        }

        var item = new Item
        {
            ItemId = _itemIdPooling.NewId(),
            RoomId = roomId,
            Type = type,
            Radius = radius,
            Speed = speed,
            Pos = pos,
            State = ItemState.Wait,
            Health = health,
            WaitTo = _timeService.GetWaitTime(1.8f) 
        };

        _itemsPooling.Add(item);

#if DEBUG
        System.Console.WriteLine("\tCreated {0}\t{1}", item.ItemId, item.Type);
#endif

        return item;
    }
}
