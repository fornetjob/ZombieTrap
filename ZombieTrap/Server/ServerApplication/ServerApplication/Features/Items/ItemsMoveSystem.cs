using Game.Core;
using ServerApplication.Features.Items;
using System;

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

            var items = _itemsPooling.Get(room.RoomId);

            #region Move

            for (int itemIndex = 0; itemIndex < items.Count; itemIndex++)
            {
                var item = items[itemIndex];

                var pos = item.Pos;

                if (item.Speed > Vector2Float.kEpsilon)
                {
                    switch (item.State)
                    {
                        case ItemState.Move:
                            pos = Vector2Float.MoveTowards(pos, item.MoveToPos, time * item.Speed);

                            if (Vector2Float.Distance(pos, item.MoveToPos) <= Vector2Float.kEpsilon)
                            {
                                pos = item.MoveToPos;

                                EndMove(item);
                            }
                            break;
                        case ItemState.Wait:
                            if (item.WaitTo < _timeService.GetGameTime())
                            {
                                var roomBound = _roomBoundService.GetRadiusBound(item.Radius);

                                var dir = _randomService.RandomDir();

                                var moveToPos = item.Pos + dir * Math.Max(roomBound.size.y, roomBound.size.x);

                                // Обрежем позицию до комнаты
                                moveToPos = new Vector2Float(
                                    Math.Min(Math.Max(moveToPos.x, roomBound.min.x), roomBound.max.x), 
                                    Math.Min(Math.Max(moveToPos.y, roomBound.min.y), roomBound.max.y));

                                dir = (item.Pos - moveToPos).normalized;

                                item.MoveToPos = moveToPos;
                                item.State = ItemState.Move;

                                continue;
                            }
                            break;
                    }
                }

                bool isIntersect = false;

                for (int otherItemIndex = itemIndex + 1; otherItemIndex < items.Count; otherItemIndex++)
                {
                    var otherItem = items[otherItemIndex];

                    var distBetweenItems = Vector2Float.Distance(pos, otherItem.Pos);

                    var distLack = distBetweenItems - item.Radius - otherItem.Radius;

                    if (distLack < 0)
                    {
                        distLack -= Vector2Float.kEpsilon;

                        if (otherItem.Speed > Vector2Float.kEpsilon)
                        {
                            otherItem.Pos = otherItem.Pos + (pos - otherItem.Pos).normalized * distLack;

                            EndMove(otherItem);
                        }

                        isIntersect = true;
                    }
                }

                if (isIntersect)
                {
                    EndMove(item);
                }

                item.Pos = pos;
            }

            #endregion
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