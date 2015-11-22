using System;
using NUnit.Framework;
using Moq;

namespace Cush.Testing.Tests.Timing
{
    [TestFixture]
    internal class StopWatchTests
    {
        [Test]
        public void TestIsRunning()
        {
            var sut = new TimeWatch();

            Assert.IsFalse(sut.IsRunning);

            sut.Start();
            Assert.IsTrue(sut.IsRunning);

            sut.Stop();
            Assert.IsFalse(sut.IsRunning);
        }

        [Test]
        public void TestIsNotHiRes_ThrowsException()
        {
            var watch = new Mock<StopwatchWrapper>();
            watch.Setup(m => m.IsHighResolution).Returns(false);

            TimeWatch sut = null;
            Assert.Throws<NotSupportedException>(() => { sut = new TimeWatch(watch.Object); });
            Assert.IsNull(sut);
        }

        [Test]
        public void TestIsHiRes_DoesNotThrowException()
        {
            var watch = new Mock<StopwatchWrapper>();
            watch.Setup(m => m.IsHighResolution).Returns(true);

            TimeWatch sut = null;
            Assert.DoesNotThrow(() => { sut = new TimeWatch(watch.Object); });
            Assert.IsNotNull(sut);
        }


        [Test]
        public void TestWrapper()
        {
            var watch = StopwatchWrapper.GetInstance();
            Assert.IsNotNull(watch);
            Assert.AreEqual(0,watch.Elapsed.TotalMilliseconds);
            
            watch.Start();
            while (watch.Elapsed.TotalMilliseconds < 500) { }
            watch.Stop();

            Assert.That(watch.Elapsed.TotalMilliseconds >= 500);
            watch.Reset();
            Assert.AreEqual(0, watch.Elapsed.TotalMilliseconds);
        }
       
    }
}