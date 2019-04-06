using Assets.Scripts.Features.Core.Zombies;
using Entitas;
using UnityEngine;

namespace Assets.Scripts.Features.Server.Zombies
{
    public class ZombiesMoveSystem : IFixedExecuteSystem
    {
        #region Services

        private GameTimeService _gameTimeService = null;
        private RandomService _randomService = null;
        private RoomBoundService _roomBoundService = null;

        #endregion

        #region Group

        [Group(GameComponentsLookup.Zombie)]
        private IGroup<GameEntity> _zombies = null;

        #endregion

        public void FixedExecute()
        {
            if (_zombies.count > 0)
            {
                var time = _gameTimeService.GetFixedDeltaTime();

                var zombieEntitas = _zombies.GetEntities();

                for (int zombieIndex = 0; zombieIndex < zombieEntitas.Length; zombieIndex++)
                {
                    var zombieEntity = zombieEntitas[zombieIndex];

                    var radius = zombieEntity.zombie.radius;
                    var pos = zombieEntity.position.value;

                    if (zombieEntity.hasMove)
                    {
                        var speed = zombieEntity.move.speed;
                        var posTo = zombieEntity.move.posTo;

                        pos = Vector3.MoveTowards(pos, posTo, time * speed);

                        if (Vector3.Distance(pos, posTo) <= Mathf.Epsilon)
                        {
                            pos = posTo;

                            EndMove(zombieEntity);
                        }
                    }
                    else
                    {
                        if (zombieEntity.hasWaitTime)
                        {
                            if (zombieEntity.waitTime.waitTo < _gameTimeService.GetGameTime())
                            {
                                zombieEntity.RemoveWaitTime();
                            }
                        }
                        else
                        {
                            AddRandomMove(zombieEntity);

                            continue;
                        }
                    }

                    bool isIntersect = false;

                    for (int otherZombieIndex = zombieIndex + 1; otherZombieIndex < zombieEntitas.Length; otherZombieIndex++)
                    {
                        var otherZombieEntity = zombieEntitas[otherZombieIndex];
                        var otherPos = otherZombieEntity.position.value;
                        var otherRadius = otherZombieEntity.zombie.radius;

                        var distBetweenZombies = Vector2.Distance(pos, otherPos);

                        if (distBetweenZombies < radius + otherRadius)
                        {
                            var distLack = distBetweenZombies - otherRadius - radius;

                            otherPos = otherPos + (pos - otherPos).normalized * distLack;

                            otherZombieEntity.ReplacePosition(otherPos);

                            EndMove(otherZombieEntity);

                            isIntersect = true;
                        }
                    }

                    if (isIntersect)
                    {
                        EndMove(zombieEntity);
                    }

                    zombieEntity.ReplacePosition(pos);
                }
            }
        }

        #region Private methods

        private void EndMove(GameEntity zombieEntity)
        {
            if (zombieEntity.hasMove)
            {
                zombieEntity.RemoveMove();

                zombieEntity.ReplaceWaitTime(_gameTimeService.GetGameTime() + _randomService.Range(0.3f, 1f));
            }
        }

        private void AddRandomMove(GameEntity entity)
        {
            var radius = entity.zombie.radius;
            var pos = entity.position.value;
            var roomBound = _roomBoundService.GetRoomBound(radius);

            var dir = _randomService.RandomDir();

            var posTo = (Vector2)pos + dir * 20;

            posTo = roomBound.ClosestPoint(posTo);

            dir = ((Vector2)pos - posTo).normalized;

            float speed;

            switch (entity.zombie.type)
            {
                case ZombieType.Small:
                    speed = 1f;
                    break;
                case ZombieType.Medium:
                    speed = 0.7f;
                    break;
                default:
                    speed = 0.4f;
                    break;
            }

            entity.AddMove(dir, posTo, speed);
        }

        #endregion
    }
}