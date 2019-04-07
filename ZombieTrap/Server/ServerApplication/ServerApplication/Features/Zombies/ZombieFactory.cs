using Assets.Scripts.Core;
using Assets.Scripts.Core.Zombies;
using ServerApplication.Features.Zombies;
using System;

public class ZombieFactory : IDependency
{
    #region Poolings

    private ZombiesPooling _zombiesPooling = null;

    #endregion

    #region Fields

    private uint _zombieId = 0;

    #endregion

    public Zombie Create(Guid roomId, ZombieType type, float radius, Vector2Float pos)
    {
        _zombieId++;

        var zombie = new Zombie
        {
            ZombieId = _zombieId,
            RoomId = roomId,
            Type = type,
            Radius = radius,
            Pos = pos,
            State = ZombieState.Wait
        };

        _zombiesPooling.Add(zombie);

#if DEBUG
        System.Console.WriteLine("\tCreated zombie {0}", zombie.ZombieId);
#endif

        return zombie;
    }
}
