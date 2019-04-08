using ServerApplication.Features.Core.Time;
using System;

public class TimeSystem : IFixedExecuteSystem, IDependencyInitialize
{
    private TimePooling _timePooling = null;

    void IDependencyInitialize.Initialize()
    {
        _timePooling.StartTime = DateTime.Now;
    }

    public void FixedExecute()
    {
        var newTime = (float)((DateTime.Now - _timePooling.StartTime).TotalMilliseconds / 1000f);

        _timePooling.DeltaTime = newTime - _timePooling.Time;

        _timePooling.Time = newTime;
    }
}