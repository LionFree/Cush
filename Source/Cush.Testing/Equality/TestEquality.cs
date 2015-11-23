//using CushTesting.Annotations;

using Cush.Testing.Assertion;

// ReSharper disable EqualExpressionComparison

namespace Cush.Testing
{

    public static partial class Cush
    {
        //[AssertionMethod]
        public static void TestEquality<T>(T item1, T item2, T item3)
        {
            TestEqualsMethod(item1, item2, item3);
            TestEqualityOperator(item1, item2, item3);
        }

        private static void TestEqualsMethod<T>(T item1, T item2, T item3)
        {
            // x.Equals(x)
            Assert.That(item1.Equals(item1));
            Assert.That(item2.Equals(item2));
            Assert.That(item3.Equals(item3));
            
            // x.Equals(y) == y.Equals(x)
            Assert.That(item1.Equals(item2) == item2.Equals(item1));
            Assert.That(item1.Equals(item3) == item3.Equals(item1));
            Assert.That(item2.Equals(item3) == item3.Equals(item2));
            
            // x.Equals(null) == false
            Assert.IsFalse(item1.Equals(null));
            Assert.IsFalse(item2.Equals(null));
            Assert.IsFalse(item3.Equals(null));

            // If x.equals(y) and y.equals(z) then x.equals(z)
            if (item1.Equals(item2) && item2.Equals(item3))
                Assert.That(item1.Equals(item3));
        }

        private static void TestEqualityOperator(object item1, object item2, object item3)
        {
            // x.Equals(x)
#pragma warning disable 1718
            Assert.That(item1 == item1);
            Assert.That(item2 == item2);
            Assert.That(item3 == item3);
#pragma warning restore 1718

            // x.Equals(y) == y.Equals(x)
            Assert.That((item1 == item2) == (item2 == item1));
            Assert.That((item1 == item3) == (item3 == item1));
            Assert.That((item3 == item2) == (item2 == item3));

            // x.Equals(null) == false
            Assert.IsFalse(item1 == null);
            Assert.IsFalse(item2 == null);
            Assert.IsFalse(item3 == null);

            // If x.equals(y) and y.equals(z) then x.equals(z)
            if (item1 == item2 && item2 == item3)
                Assert.That(item1 == item3);
        }
    }
}
