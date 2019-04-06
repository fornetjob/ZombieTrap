public class GameTimeEvent
{
    private readonly object
        _lockObj = new object();

    private readonly GameEntity
        _timeEntity;

    private readonly float
        _secondsInterval;

    private float
        _timeTo;

    public GameTimeEvent(GameEntity timeEntity, float secondsInterval)
    {
        _timeEntity = timeEntity;
        _secondsInterval = secondsInterval;

        Reset();
    }

    public void Reset()
    {
        lock (_lockObj)
        {
            _timeTo = _timeEntity.gameTime.value + _secondsInterval;
        }
    }

    public bool Check()
    {
        lock (_lockObj)
        {
            bool isTimeOver = _timeTo <= _timeEntity.gameTime.value;

            if (isTimeOver)
            {
                Reset();
            }

            return isTimeOver;
        }
    }
}