using System;

namespace CrankEngine.Engine
{
    public class Time
    {
        public static long StartTime = 0;
        public static long RunTime = 0;
        public static float DeltaTime = 0;

        public static void StartClock()
        {
            StartTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        public static void TickTime()
        {
            DeltaTime = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()-(StartTime+RunTime))/1000.0f;
            RunTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()-StartTime;
        }
    }
}
