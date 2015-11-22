using System;
using NUnit.Framework;

namespace Cush.Testing.Tests.Timing
{
    [TestFixture]
    internal class ClockTests
    {
        [Test]
        public void Test_Clock_IStopWatch()
        {
            Assert.DoesNotThrow(
                () =>
                {
                    Clock.WarmupTime = 250;
                    Clock.Iterations = 5;
                    Clock.Benchmark<TimeWatch>(
                        () => GetRandom.Double(double.MinValue, double.MaxValue, Scale.Exponential));
                });
            //Assert.AreNotEqual(actual, 0, "Benchmarked time returned zero.");
        }


        [Test]
        public void Test_Clock_Time()
        {
            Assert.DoesNotThrow(
                () =>
                {
                    Clock.WarmupTime = 0;
                    Clock.Iterations = 5;
                    Clock.BenchmarkTime(
                        () => GetRandom.Double(double.MinValue, double.MaxValue, Scale.Exponential));
                });
            //Assert.AreNotEqual(actual, 0, "Benchmarked time returned zero.");
        }

        [Test]
        public void Test_Clock_Time_WithParameters()
        {
            var actual = new double();
            Assert.DoesNotThrow(
                () =>
                {
                    Clock.WarmupTime = 0;
                    Clock.Iterations = 5;
                    Clock.BenchmarkTime(
                        () => actual = GetRandom.Double(double.MinValue, double.MaxValue, Scale.Exponential), 10, true);
                });
            Assert.AreNotEqual(actual, 0, "Benchmarked time returned zero.");
        }

        [Test]
        public void Test_Clock_CPU()
        {
            Assert.DoesNotThrow(
                () =>
                {
                    Clock.WarmupTime = 0;
                    Clock.Iterations = 5;
                    Clock.BenchmarkCpu(() => GetRandom.Double(double.MinValue, double.MaxValue, Scale.Exponential));
                });
            //Assert.AreNotEqual(actual, 0, "Benchmarked time returned zero.");
        }


        [Test]
        public void Test_Clock_ConfigureForMobile()
        {
            Assert.DoesNotThrow(Clock.ConfigureForMobile);
            Assert.AreEqual(Clock.MobileWarmup, Clock.WarmupTime,
                "Clock Warmup time {0} was not set to the proper time {1}", Clock.WarmupTime, Clock.MobileWarmup);
        }

        [Test]
        public void Test_Clock_ConfigureForLaptop()
        {
            Assert.DoesNotThrow(Clock.ConfigureForLaptop);
            Assert.AreEqual(Clock.MobileWarmup, Clock.WarmupTime,
               "Clock Warmup time {0} was not set to the proper time {1}", Clock.WarmupTime, Clock.MobileWarmup);
        }

        [Test]
        public void Test_Clock_ConfigureForDesktop()
        {
            Assert.DoesNotThrow(Clock.ConfigureForDesktop);
            Assert.AreEqual(Clock.DesktopWarmup, Clock.WarmupTime,
               "Clock Warmup time {0} was not set to the proper time {1}", Clock.WarmupTime, Clock.DesktopWarmup);
        }


        [Test]
        public void Test_Clock_GetPercentChange_NullTicks()
        {
            Assert.Throws<ArgumentNullException>(() => Clock.GetPercentChange(null));
        }

        [Test]
        public void Test_Clock_GetPercentChange_EmptyTicks()
        {
            Assert.Throws<ArgumentException>(() => Clock.GetPercentChange(new long[] {}));
        }

        [Test]
        public void Test_Clock_GetPercentChange_TicksOutOfOrder()
        {
            var actual = new double();
            
            Assert.DoesNotThrow(() => actual = Clock.GetPercentChange(new long[] {1, 5, 7, 3, 2, 4, 8}));
            Assert.IsNotNull(actual, "Method returned null value.");
            Assert.AreNotEqual(0, actual, "Method erroneously returned actual value of zero.");
        }
    }
}