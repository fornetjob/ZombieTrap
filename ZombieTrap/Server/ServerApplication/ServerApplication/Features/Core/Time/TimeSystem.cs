using ServerApplication.Features.Core.Time;

public class TimeSystem : IFixedExecuteSystem
{
    private TimeService _timeService = null;
    private TimePooling _timePooling = null;

    public void FixedExecute()
    {
        _timePooling.Value += _timeService.GetFixedDeltaTime();
    }
}