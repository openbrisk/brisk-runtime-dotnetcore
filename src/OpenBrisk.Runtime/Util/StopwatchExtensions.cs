namespace OpenBrisk.Runtime.Utils
{
    using System.Diagnostics;

    public static class StopwatchExtensions
    {
        public static long ElapsedNanoSeconds(this Stopwatch watch)
        {
            return watch.ElapsedTicks * 1_000_000_000 / Stopwatch.Frequency;
        }

        public static long ElapsedMicroSeconds(this Stopwatch watch)
        {
            return watch.ElapsedTicks * 1_000_000 / Stopwatch.Frequency;
        }
    } 
}