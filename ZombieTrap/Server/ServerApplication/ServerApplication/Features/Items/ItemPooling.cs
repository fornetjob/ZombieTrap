using ServerApplication.Features.Items;
using System;
using System.Collections.Generic;

public class ItemsPooling : IDependency
{
    private WeakDictionary<Guid, List<Item>>
        _items = new WeakDictionary<Guid, List<Item>>((roomId) => new List<Item>());

    public int GetCount(Guid roomId)
    {
        return _items[roomId].Count;
    }

    public void Add(Item zombie)
    {
        _items[zombie.RoomId].Add(zombie);
    }

    public List<Item> Get(Guid roomId)
    {
        return _items[roomId];
    }
}