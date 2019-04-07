using Assets.Scripts.Core;
using ServerApplication.Features.Zombies;

public class ZombiesMoveSystem : IFixedExecuteSystem
{
    #region Services

    private RoomBoundService _roomBoundService = null;
    private TimeService _timeService = null;
    private RandomService _randomService = null;

    #endregion

    #region Poolings

    private RoomsPooling _roomsPooling = null;
    private ZombiesPooling _zombiesPooling = null;

    #endregion

    public void FixedExecute()
    {
        var time = _timeService.GetFixedDeltaTime();

        for (int roomIndex = 0; roomIndex < _roomsPooling.Rooms.Count; roomIndex++)
        {
            var room = _roomsPooling.Rooms[roomIndex];

            var zombies = _zombiesPooling.GetZombies(room.RoomId);

            for (int zombieIndex = 0; zombieIndex < zombies.Count; zombieIndex++)
            {
                var zombie = zombies[zombieIndex];

                var pos = zombie.Pos;

                switch (zombie.State)
                {
                    case ZombieState.Move:
                        pos = Vector2Float.MoveTowards(pos, zombie.MoveToPos, time * zombie.Speed);

                        if (Vector2Float.Distance(pos, zombie.MoveToPos) <= Vector2Float.kEpsilon)
                        {
                            pos = zombie.MoveToPos;

                            EndMove(zombie);
                        }
                        break;
                    case ZombieState.Wait:
                        if (zombie.WaitTo < _timeService.GetGameTime())
                        {
                            var roomBound = _roomBoundService.GetBound(zombie.Radius);

                            var dir = _randomService.RandomDir();

                            var moveToPos = zombie.Pos + dir * roomBound.size.x;

                            moveToPos = roomBound.ClosestPoint(moveToPos);

                            dir = (zombie.Pos - moveToPos).normalized;

                            zombie.MoveToPos = moveToPos;
                            zombie.State = ZombieState.Move;

                            continue;
                        }
                        break;
                }

                bool isIntersect = false;

                for (int otherZombieIndex = zombieIndex + 1; otherZombieIndex < zombies.Count; otherZombieIndex++)
                {
                    var otherZombie = zombies[otherZombieIndex];

                    var distBetweenZombies = Vector2Float.Distance(pos, otherZombie.Pos);

                    if (distBetweenZombies < zombie.Radius + otherZombie.Radius)
                    {
                        var distLack = distBetweenZombies - zombie.Radius - otherZombie.Radius;

                        otherZombie.Pos = otherZombie.Pos + (pos - otherZombie.Pos).normalized * distLack;

                        EndMove(otherZombie);

                        isIntersect = true;
                    }
                }

                if (isIntersect)
                {
                    EndMove(zombie);
                }

                zombie.Pos = pos;
            }
        }
    }

    private void EndMove(Zombie zombie)
    {
        if (zombie.State == ZombieState.Move)
        {
            zombie.State = ZombieState.Wait;
            zombie.WaitTo = _timeService.GetGameTime() + _randomService.Range(0.3f, 1f);
        }
    }
}