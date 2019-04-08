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
            features.Add(new ListeningSystem());
            features.Add(new SenderSystem());

            int fixedTimeMs = features.Dependencies.Provide<TimeService>().GetFixedDeltaTimeMs();

            while (true)
            {
                features.FixedExecute();

                Thread.Sleep(fixedTimeMs);

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
