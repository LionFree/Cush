using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Cush.Common.Tests
{
#pragma warning disable 1718
    [TestFixture]
    [SuppressMessage("ReSharper", "EqualExpressionComparison")]
    internal class EquatableBaseTests
    {
        [Test]
        public void Test_EqualsOperator_Equals()
        {
            var item1 = new TestEquatableClass();
            var item2 = new TestEquatableClass();

            Assert.IsTrue(item1 == item2);
            Assert.IsFalse(item1 != item2);
        }

        [Test]
        public void Test_EqualsOperator_OneNullObject()
        {
            var item1 = new TestEquatableClass();
            TestEquatableClass item2 = null;

            Assert.IsFalse(item1 == item2);
            Assert.IsTrue(item1 != item2);
        }

        [Test]
        public void Test_EqualsOperator_ReferenceEquals()
        {
            var item1 = new TestEquatableClass();
            Assert.IsTrue(item1 == item1);
            Assert.IsFalse(item1 != item1);
        }
    }
}