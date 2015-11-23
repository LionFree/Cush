using Cush.Testing.Tests.Fakes;
using NUnit.Framework;
using AssertionException = Cush.Testing.Assertion.Exceptions.AssertionException;

namespace Cush.Testing.Tests.Equality
{
    [TestFixture]
    internal class TestGetHashcodeTests
    {
        [Test]
        public void TestGetHashcode_fail()
        {
            var item1 = new EqualityHarness(true, true, false);
            var item2 = item1;

            Assert.That(item1.Equals(item2));
            Assert.That(item1.GetHashCode() != item2.GetHashCode());
            Assert.Throws<AssertionException>(() => Cush.TestGetHashCode(item1, item2));
        }

        [Test]
        public void TestGetHashcode_pass()
        {
            var item1 = new EqualityHarness(true, true);
            var item2 = item1;

            Assert.That(item1.Equals(item2));
            Assert.That(item1.GetHashCode() == item2.GetHashCode());
            Assert.DoesNotThrow(() => Cush.TestGetHashCode(item1, item2));
        }
    }
}