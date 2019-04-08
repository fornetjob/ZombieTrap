using Game.Core;
using ServerApplication.Features.Items;

public class ItemsMoveSystem : IFixedExecuteSystem
{
    #region Services

    private RoomBoundService _roomBoundService = null;
    private TimeService _timeService = null;
    private RandomService _randomService = null;

    #endregion

    #region Poolings

    private RoomsPooling _roomsPooling = null;
    private ItemsPooling _itemsPooling = null;

    #endregion

    public void FixedExecute()
    {
        var time = _timeService.GetFixedDeltaTime();

        for (int roomIndex = 0; roomIndex < _roomsPooling.Rooms.Count; roomIndex++)
        {
            var room = _roomsPooling.Rooms[roomIndex];

            var zombies = _itemsPooling.Get(room.RoomId);

            for (int zombieIndex = 0; zombieIndex < zombies.Count; zombieIndex++)
            {
                var zombie = zombies[zombieIndex];

                var pos = zombie.Pos;

                if (zombie.Speed > Vector2Float.kEpsilon)
                {
                    switch (zombie.State)
                    {
                        case ItemState.Move:
                            pos = Vector2Float.MoveTowards(pos, zombie.MoveToPos, time * zombie.Speed);

                            if (Vector2Float.Distance(pos, zombie.MoveToPos) <= Vector2Float.kEpsilon)
                            {
                                pos = zombie.MoveToPos;

                                EndMove(zombie);
                            }
                            break;
                        case ItemState.Wait:
                            if (zombie.WaitTo < _timeService.GetGameTime())
                            {
                                var roomBound = _roomBoundService.GetRadiusBound(zombie.Radius);

                                var dir = _randomService.RandomDir();

                                var moveToPos = zombie.Pos + dir * roomBound.size.x;

                                moveToPos = roomBound.ClosestPoint(moveToPos);

                                dir = (zombie.Pos - moveToPos).normalized;

                                zombie.MoveToPos = moveToPos;
                                zombie.State = ItemState.Move;

                                continue;
                            }
                            break;
                    }
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

    private void EndMove(Item zombie)
    {
        if (zombie.State == ItemState.Move)
        {
            zombie.State = ItemState.Wait;
            zombie.WaitTo = _timeService.GetGameTime() + _randomService.Range(0.3f, 1f);
        }
    }
}