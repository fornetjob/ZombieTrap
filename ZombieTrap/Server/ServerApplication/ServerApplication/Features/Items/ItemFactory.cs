using Game.Core;
using ServerApplication.Features.Items;
using System;

public class ItemFactory : IDependency
{
    #region Poolings

    private ItemsPooling _itemsPooling = null;

    #endregion

    #region Fields

    private uint _itemId = 0;

    #endregion

    public Item Create(Guid roomId, ItemType type, float radius, Vector2Float pos)
    {
        _itemId++;

        var item = new Item
        {
            ItemId = _itemId,
            RoomId = roomId,
            Type = type,
            Radius = radius,
            Pos = pos,
            State = ItemState.Wait
        };

        _itemsPooling.Add(item);

#if DEBUG
        System.Console.WriteLine("\tCreated {0}\t{1}", item.ItemId, item.Type);
#endif

        return item;
    }
}
