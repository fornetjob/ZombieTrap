using ServerApplication.Features.Items;
using System;
using System.Collections.Generic;

using Game.Core;

public class ItemsPooling : IDependency
{
    private WeakDictionary<Guid, List<Item>>
        _items = new WeakDictionary<Guid, List<Item>>((roomId) => new List<Item>());

    private WeakDictionary<Guid, List<Item>>
        _zombiesItems = new WeakDictionary<Guid, List<Item>>((roomId) => new List<Item>());

    public void Add(Item item)
    {
        _items[item.RoomId].Add(item);

        if (item.Type.IsZombie())
        {
            _zombiesItems[item.RoomId].Add(item);
        }
    }

    public List<Item> Get(Guid roomId)
    {
        return _items[roomId];
    }

    public List<Item> GetZombies(Guid roomId)
    {
        return _zombiesItems[roomId];
    }
}