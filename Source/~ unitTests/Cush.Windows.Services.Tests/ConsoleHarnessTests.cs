using Cush.Common.Logging;
using Moq;
using NUnit.Framework;

namespace Cush.Windows.Services.Tests
{
    [TestFixture]
    public class ConsoleHarnessTests
    {
        [Test]
        public void Run()
        {
            var runned = false;
            var service = new TestService(Loggers.Trace);
            var thing = new Mock<IConsoleHarness>();
            thing.Setup(m => m.Run(It.IsAny<string[]>(), It.IsAny<WindowsService>())).Callback(() => { runned = true; });

            thing.Object.Run(new[] {"asdf"}, service);

            Assert.IsTrue(runned);

        }
    }
}