using UnityEngine;

public class RoomBoundService : IService
{
    #region Fields

    private WeakDictionary<float, Bounds>
        _roomBounds = new WeakDictionary<float, Bounds>((radius) =>
        {
            var bound = RoomFactory.RoomBound;
            bound.Expand(radius * -2f);
            return bound;
        });

    #endregion

    public Bounds GetRoomBound(float zombieRadius)
    {
        return _roomBounds[zombieRadius];
    }
}