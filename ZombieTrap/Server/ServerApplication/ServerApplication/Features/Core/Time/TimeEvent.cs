using ServerApplication.Features.Core.Time;

public class TimeEvent
{
    private readonly object
        _lockObj = new object();

    private readonly TimePooling
        _timePooling;

    private readonly float
        _secondsInterval;

    private float
        _timeTo;

    public TimeEvent(TimePooling timePooling, float secondsInterval)
    {
        _timePooling = timePooling;
        _secondsInterval = secondsInterval;

        Reset();
    }

    public void Reset()
    {
        lock (_lockObj)
        {
            _timeTo = _timePooling.Value + _secondsInterval;
        }
    }

    public bool Check()
    {
        lock (_lockObj)
        {
            bool isTimeOver = _timeTo <= _timePooling.Value;

            if (isTimeOver)
            {
                Reset();
            }

            return isTimeOver;
        }
    }
}