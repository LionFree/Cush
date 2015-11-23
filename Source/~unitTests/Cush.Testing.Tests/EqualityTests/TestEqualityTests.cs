using Cush.Testing.Tests.Fakes;
using NUnit.Framework;

namespace Cush.Testing.Tests.Equality
{
    [TestFixture]
    internal class TestEqualityTests
    {
        [Test]
        public void TestEqualityTest_EqualsFail()
        {
            // Make sure TestEquality throws an exception when Equals() is wrong.
            var item1 = new EqualityHarness(false, true);
            var item2 = item1;
            var item3 = item1;

            Assert.That(!item1.Equals(item2));

            Assert.Throws<Assertion.Exceptions.AssertionException>(() => Cush.TestEquality(item1, item2, item3));
        }

        [Test]
        public void TestEqualityTest_OperatorFail()
        {
            // Make sure TestEquality throws an exception when Equals() is wrong.
            var item1 = new EqualityHarness(true, false);
            var item2 = item1;
            var item3 = item1;

            Assert.That(item1 != item2);

            Assert.Throws<Assertion.Exceptions.AssertionException>(() => Cush.TestEquality(item1, item2, item3));
        }

        [Test]
        public void TestEqualityTest_pass()
        {
            // Make sure TestEquality is good for something where Equals() works.
            var item1 = GetRandom.String();
            var item2 = item1;
            var item3 = item1;

            Assert.DoesNotThrow(() => Cush.TestEquality(item1, item2, item3));
        }
    }
}