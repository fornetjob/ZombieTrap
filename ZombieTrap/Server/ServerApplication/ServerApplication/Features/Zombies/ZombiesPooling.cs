using ServerApplication.Features.Zombies;
using System;
using System.Collections.Generic;

public class ZombiesPooling : IDependency
{
    private WeakDictionary<Guid, List<Zombie>>
        _zombies = new WeakDictionary<Guid, List<Zombie>>((roomId) => new List<Zombie>());

    public int GetCount(Guid roomId)
    {
        return _zombies[roomId].Count;
    }

    public void Add(Zombie zombie)
    {
        _zombies[zombie.RoomId].Add(zombie);
    }

    public List<Zombie> GetZombies(Guid roomId)
    {
        return _zombies[roomId];
    }
}