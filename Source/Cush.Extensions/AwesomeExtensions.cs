using System;

namespace Cush.Extensions
{
    public static class AwesomeExtensions
    {
        #region String.Format

        // Enable quick and more natural string.Format calls
        public static string Format(this string s, params object[] args)
        {
            return string.Format(s, args);
        }

        #endregion

        #region Random.OneOf("John", "Ringo", "Paul")

        public static T OneOf<T>(this Random rng, params T[] things)
        {
            return things[rng.Next(things.Length)];
        }

        #endregion

        #region number.IsBetween(lower, upper)

        public static bool IsBetween<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
            return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) < 0;
        }

        #endregion

        #region MiscUtil

        /// <summary>
        ///     Throws an ArgumentNullException if the given string is null or empty.
        /// </summary>
        /// <param name="value">The string to check for nullity or emptiness.</param>
        /// <param name="message">The name to use when throwing an exception, if necessary</param>
        public static void ThrowIfNullOrEmpty(this string value, string message)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(message);
            }
        }

        /// <summary>
        ///     Throws an ArgumentNullException if the given data item is null.
        /// </summary>
        /// <param name="data">The item to check for nullity.</param>
        /// <param name="message">The message to show when throwing an exception, if necessary.</param>
        public static void ThrowIfNull<T>(this T data, string message) where T : class
        {
            if (data == null)
            {
                throw new ArgumentNullException(message);
            }
        }

        /// <summary>
        ///     Throws an ArgumentNullException if the given data item is null.
        ///     No parameter name is specified.
        /// </summary>
        /// <param name="data">The item to check for nullity.</param>
        public static void ThrowIfNull<T>(this T data) where T : class
        {
            if (data == null)
            {
                throw new ArgumentNullException();
            }
        }

        #endregion
    }
}