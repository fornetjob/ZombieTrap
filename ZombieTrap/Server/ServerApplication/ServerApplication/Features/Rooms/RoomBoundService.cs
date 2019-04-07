using Assets.Scripts.Core;

public class RoomBoundService:IService, IDependencyInitialize
{
    #region Services

    private SettingsService _settingsService = null;
    
    #endregion

    #region Fields

    private WeakDictionary<float, RectangleFloat> _bounds;

    #endregion

    #region IDependencyInitialize

    void IDependencyInitialize.Initialize()
    {
        var bound = new RectangleFloat(Vector2Float.zero, 
            new Vector2Float(_settingsService.GetRoomWidth(), _settingsService.GetRoomHeight()));

        _bounds = new WeakDictionary<float, RectangleFloat>((radius) => bound.Expand(radius * -2f));
    }

    #endregion

    public RectangleFloat GetBound(float radius)
    {
        return _bounds[radius];
    }
}