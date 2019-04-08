using ServerApplication.Features;

using System;
using System.Threading;

namespace ServerApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var features = new ServerFeatures();

            features.Add(new TimeSystem());
            features.Add(new RoomSystem());
            features.Add(new ZombieSpawnSystem());
            features.Add(new ItemsMoveSystem());
            features.Add(new ItemsSendPositionsSystem());
            features.Add(new ListeningSystem());
            features.Add(new SenderSystem());

            var fixedTime = features.Dependencies.Provide<SettingsService>().GetFixedDeltaTime();

            var timeService = features.Dependencies.Provide<TimeService>();

            while (true)
            {
                features.FixedExecute();

                var deltaTime = timeService.GetDeltaTime();

                Thread.Sleep(Math.Max(1, Convert.ToInt32((fixedTime - deltaTime) * 1000)));

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
            }
        }
    }
}