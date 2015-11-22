using System;
using System.Diagnostics;
using Cush.Windows;
using Cush.Common.Exceptions;


namespace Cush.TestHarness.ConsoleApp.Infrastructure
{
    internal static class TestMethods
    {
        internal static void Test_ExceptionHandling()
        {
            // Console apps don't have a UI thread. 
            // Any exception will crash the app.
            var divisor = 32;
            var dividend = 0;
            var quotient = divisor/dividend;

            Console.WriteLine(quotient);
        }

        internal static void Test_ApplicationType()
        {
            var type = ApplicationType.Current;
            Trace.WriteLine(type);
        }

        internal static void Test_ThrowHelper(string scooby)
        {
            ThrowHelper.IfNullThenThrow(() => scooby);
        }

        internal static void Test_Benchmarks()
        {
            //Clock.ConfigureForLaptop();
            //var elapsedCpuTime =
            //    Clock.BenchmarkCpu(() => GetRandom.Double(double.MinValue, double.MaxValue, Scale.Exponential));

            //var elapsedClockTime =
            //    Clock.BenchmarkTime(() => GetRandom.Double(double.MinValue, double.MaxValue, Scale.Exponential));
        }
    }
}