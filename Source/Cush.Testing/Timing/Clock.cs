using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Cush.Testing
{
    public class Clock
    {
        public const int MobileWarmup = 10000;
        public const int DesktopWarmup = 1500;
        public const int DefaultRepetitions = 20;
        public const int DefaultIterations = 100000;

        static Clock()
        {
            WarmupTime = DesktopWarmup;
            Repetitions = DefaultRepetitions;
            Iterations = DefaultIterations;
        }
        
        public static int WarmupTime { get; set; }
        public static int Repetitions { get; set; }
        public static int Iterations { get; set; }

        public static void ConfigureForMobile()
        {
            WarmupTime = MobileWarmup;
        }

        public static void ConfigureForLaptop()
        {
            ConfigureForMobile();
        }

        public static void ConfigureForDesktop()
        {
            WarmupTime = DesktopWarmup;
        }

        public static double BenchmarkTime(Action action)
        {
            return BenchmarkTime(action, Iterations);
        }

        public static double BenchmarkTime(Action action, int iterations)
        {
            return BenchmarkTime(action, iterations, false);
        }

        public static double BenchmarkTime(Action action, int iterations, bool showSteps)
        {
            return Benchmark<TimeWatch>(action, iterations, showSteps);
        }

        public static double BenchmarkCpu(Action action)
        {
            return BenchmarkCpu(action, Iterations);
        }

        public static double BenchmarkCpu(Action action, int iterations)
        {
            return BenchmarkCpu(action, iterations, false);
        }

        public static double BenchmarkCpu(Action action, int iterations, bool showSteps)
        {
            return Benchmark<CpuWatch>(action, iterations, showSteps);
        }

        public static double Benchmark<TStopwatch>(Action action) where TStopwatch : IStopWatch, new()
        {
            return Benchmark<TStopwatch>(action, Iterations);
        }

        public static double Benchmark<TStopwatch>(Action action, int iterations) where TStopwatch : IStopWatch, new()
        {
            return Benchmark<TStopwatch>(action, iterations, false);
        }

        public static double Benchmark<TStopWatch>(Action action, int iterations, bool showSteps)
            where TStopWatch : IStopWatch, new()
        {
            var stopwatch = new TStopWatch();
            var ticks = new long[Repetitions];
            var timings = new double[Repetitions];

            SetHighPriority();
            CollectGarbage();

            //prevent the JIT Compiler from optimizing Fkt calls away
            long seed = Environment.TickCount;
            Trace.WriteLine("TickCount: " + seed);

            //WarmUp(stopwatch, action);
            Console.Write(Strings.INFO_WarmingUp);
            stopwatch.Reset();
            stopwatch.Start();
            while (stopwatch.Elapsed.TotalMilliseconds < WarmupTime)
            {
                action(); // Warmup
            }
            stopwatch.Stop();
            Console.WriteLine(Strings.INFO_Ready);

            // Start timing.
            for (var i = 0; i < Repetitions; i++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (var j = 0; j < iterations; j++)
                    action();
                stopwatch.Stop();

                ticks[i] = stopwatch.Elapsed.Ticks;
                timings[i] = stopwatch.Elapsed.TotalMilliseconds;
            }

            if (showSteps)
            {
                for (var i = 0; i < Repetitions; i++)
                {
                    Console.WriteLine(Strings.INFO_Timing, i, ticks[i],
                        Math.Round(timings[i], 3));
                }
            }

            var delta = GetPercentChange(ticks);
            Console.WriteLine(Strings.Benchmark_Results,
                typeof (TStopWatch), iterations, Math.Round(timings.NormalizedMean(), 3), delta);

            return timings.NormalizedMean();
        }

        private static void SetHighPriority()
        {
            //use the second Core/Processor for the test
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);

            //prevent "Normal" Processes from interrupting Threads
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            //prevent "Normal" Threads from interrupting this thread
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
        }

        private static void CollectGarbage()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        internal static double GetPercentChange(IReadOnlyList<long> ticks)
        {
            if (null == ticks) throw new ArgumentNullException("ticks");
            if (ticks.Count == 0) throw new ArgumentException(Strings.ERROR_NoTimingsPassed, "ticks");

            var lowest = ticks[0];
            var highest = ticks[0];
            foreach (var t in ticks)
            {
                if (t < lowest) lowest = t;
                if (t > highest) highest = t;
            }

            if (lowest == 0) return 0;

            var value = (double) (highest - lowest)/lowest;
            return Math.Round(value*100, 2);
        }
    }
}