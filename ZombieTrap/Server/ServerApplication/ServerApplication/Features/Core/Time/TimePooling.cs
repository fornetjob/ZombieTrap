using System;

namespace ServerApplication.Features.Core.Time
{
    public class TimePooling:IDependency
    {
        public DateTime StartTime;
        public float Time;
        public float DeltaTime;
    }
}
