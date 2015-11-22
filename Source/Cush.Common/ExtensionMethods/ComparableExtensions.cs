using System;
using System.Diagnostics;

namespace Cush.Common
{
    public static class ComparableExtensions
    {
        public static bool IsBetween<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
            return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) < 0;
        }

        [Conditional("NeverCompile")]
        private static void Test_IsBetween()
        {
            const int number = 14;
            var truth = number.IsBetween(10, 17);
        }
    }
}