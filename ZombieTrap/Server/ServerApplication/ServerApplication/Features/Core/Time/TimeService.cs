using ServerApplication.Features.Core.Time;

public class TimeService : IService
{
    #region Poolings

    private TimePooling _timePooling = null;

    #endregion

    public float GetGameTime()
    {
        return _timePooling.Time;
    }

    public float GetDeltaTime()
    {
        return _timePooling.DeltaTime;
    }

    public TimeEvent CreateTimeEvent(float timeInterval)
    {
        return new TimeEvent(_timePooling, timeInterval);
    }
}