using NUnit.Framework;

namespace Cush.Testing.Tests.ExtensionsTests
{
    [TestFixture]
    public class ComparisonExtensionsTests
    {
        [TestCase(1000)]
        public void Test_IsGreaterThan(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var item1 = GetRandom.Int(0, 10);
                var item2 = GetRandom.Int(20, 30);

                Assert.That(item2.IsGreaterThan(item1), "Value is less than minimum.");
            }
        }

        [TestCase(1000)]
        public void Test_IsLessThan(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var item1 = GetRandom.Int(0, 10);
                var item2 = GetRandom.Int(20, 30);

                Assert.That(item1.IsLessThan(item2), "Value is more than maximum.");
            }
        }

        [TestCase(1000)]
        public void Test_IsAtMost(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var item1 = GetRandom.Int(0, 2);
                Assert.That(item1.IsAtMost(1), "Value is more than maximum.");
            }
        }

        [TestCase(1000)]
        public void Test_IsAtLeast(int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                var item1 = GetRandom.Int(0, 2);
                Assert.That(item1.IsAtLeast(0), "Value is less than minimum.");
            }
        }
    }
}