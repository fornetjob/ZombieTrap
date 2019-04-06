using Assets.Scripts.Features.Core.Zombies;
using Entitas;
using UnityEngine;

public class ZombieService : IService
{
    #region Factories

    private ServerZombieFactory _serverZombieFactory = null;
    private RandomService _randomService = null;

    #endregion

    #region Groups

    [Group(GameComponentsLookup.Zombie)]
    private IGroup<GameEntity> _zombies = null;

    #endregion

    public bool IsWantSpawn(ServerSideEntity roomEntity)
    {
        return _zombies.count < roomEntity.room.MaxZombieCount;
    }

    public bool IsZombieIntersect(Vector3 pos, float radius)
    {
        if (_zombies.count > 0)
        {
            var zombies = _zombies.GetEntities();

            for (int i = 0; i < zombies.Length; i++)
            {
                var zombie = zombies[i];

                var otherPos = zombie.position.value;
                var otherRadius = zombie.zombie.radius;

                var distBetweenZombies = Vector2.Distance(pos, otherPos);

                if (distBetweenZombies < radius + otherRadius)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void SpawnZombie(ServerSideEntity roomEntity)
    {
        float radius;
        ZombieType type;

        switch (_randomService.Range(0, 3))
        {
            case 0:
                type = ZombieType.Small;
                radius = 0.7f;
                break;
            case 1:
                type = ZombieType.Medium;
                radius = 1f;
                break;
            default:
                type = ZombieType.Big;
                radius = 1.3f;
                break;
        }

        var spawnBound = roomEntity.bound.value;
        spawnBound.Expand(radius * -1);

        var pos = _randomService.RandomPos(spawnBound);

        if (IsZombieIntersect(pos, radius) == false)
        {
            roomEntity.ReplaceIdentity(roomEntity.identity.value + 1);

            _serverZombieFactory.Create(roomEntity.identity.value, type, radius, pos);
        }
    }
}