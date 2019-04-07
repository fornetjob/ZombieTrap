using ServerApplication.Features.Core.Time;

using System;
using System.Configuration;

public class TimeService : IService, IDependencyInitialize
{
    #region Services

    private SettingsService _settingsService = null;

    #endregion

    #region Poolings

    private TimePooling _timePooling = null;

    #endregion

    #region Fields

    private float _fixedDeltaTime;

    #endregion

    #region IDependencyInitialize

    void IDependencyInitialize.Initialize()
    {
        _fixedDeltaTime = _settingsService.GetFixedDeltaTimeMs() / 1000f;
    }
    
    #endregion

    public float GetGameTime()
    {
        return _timePooling.Value;
    }

    public float GetFixedDeltaTime()
    {
        return _fixedDeltaTime;
    }

    public int GetFixedDeltaTimeMs()
    {
        return System.Convert.ToInt32(_fixedDeltaTime * 1000);
    }

    public TimeEvent CreateTimeEvent(float timeInterval)
    {
        return new TimeEvent(_timePooling, timeInterval);
    }

    
}