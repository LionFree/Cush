using Cush.Testing.Assertion;

namespace Cush.Testing
{
    public static partial class Cush
    {
        public static void TestGetHashCode(object item1, object item2)
        {
            // If two distinct objects compare as equal, their hashcodes must be equal.
            if (item1.Equals(item2))
                Assert.That(item1.GetHashCode() == item2.GetHashCode());
        }
    }
}
