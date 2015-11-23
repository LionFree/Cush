using System;
using System.Diagnostics;

namespace Cush.Testing
{
    [DebuggerStepThrough]
    public static class ComparisonExtensions
    {
        public static bool IsGreaterThan<T>(this T value, T other) where T : IComparable
        {
            return value.CompareTo(other) > 0;
        }

        public static bool IsAtLeast<T>(this T value, T other) where T : IComparable
        {
            return value.CompareTo(other) >= 0;
        }

        public static bool IsLessThan<T>(this T value, T other) where T : IComparable
        {
            return value.CompareTo(other) < 0;
        }

        public static bool IsAtMost<T>(this T value, T other) where T : IComparable
        {
            return value.CompareTo(other) <= 0;
        }
    }
}