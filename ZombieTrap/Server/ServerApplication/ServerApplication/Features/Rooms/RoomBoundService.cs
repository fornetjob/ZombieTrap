using Game.Core;

public class RoomBoundService:IService, IDependencyInitialize
{
    #region Services

    private SettingsService _settingsService = null;
    
    #endregion

    #region Fields

    private WeakDictionary<float, RectangleFloat> _bounds;

    private RectangleFloat _roomBound;

    #endregion

    #region IDependencyInitialize

    void IDependencyInitialize.Initialize()
    {
        _roomBound = new RectangleFloat(Vector2Float.zero, 
            new Vector2Float(_settingsService.GetRoomWidth(), _settingsService.GetRoomHeight()));

        _bounds = new WeakDictionary<float, RectangleFloat>((radius) => _roomBound.Expand(radius * -2f));
    }

    #endregion

    public RectangleFloat GetRoomBound()
    {
        return _roomBound;
    }

    public RectangleFloat GetRadiusBound(float radius)
    {
        return _bounds[radius];
    }
}