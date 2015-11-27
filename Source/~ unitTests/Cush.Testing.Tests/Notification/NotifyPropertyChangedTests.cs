using Cush.Testing.Tests.Fakes;
using NUnit.Framework;

namespace Cush.Testing.Tests
{
    [TestFixture]
    internal class NotifyPropertyChangedTests
    {
        [Test]
        public void NPC_ShouldNotNotifyOn_correctUsage()
        {
            var item = new PropertyChanger();
            Assert.DoesNotThrow(() => item.ShouldNotNotifyOn(m => m.Property1).When(m => m.Property2 = 5));
        }

        [Test]
        public void NPC_ShouldNotNotifyOn_incorrectUsage()
        {
            var item = new PropertyChanger();
            Assert.Throws<Assertion.Exceptions.AssertionException>(() => item.ShouldNotNotifyOn(m => m.Property1).When(m => m.Property1 = 5));
        }

        [Test]
        public void NPC_ShouldNotifyOn_correctUsage()
        {
            var item = new PropertyChanger();
            Assert.DoesNotThrow(() =>
                item.ShouldNotifyOn(m => m.Property1).When(m => m.Property1 = 5)
                );
        }

        [Test]
        public void NPC_ShouldNotifyOn_incorrectUsage()
        {
            var item = new PropertyChanger();
            Assert.Throws<Assertion.Exceptions.AssertionException>(() => item.ShouldNotifyOn(m => m.Property1).When(m => m.Property2 = 5));
        }
    }
}