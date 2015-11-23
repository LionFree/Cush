using System;
using System.Diagnostics;

namespace Cush.Common
{
    public static class EnumExtensions
    {
        /// <summary>
        ///     Parses a string into an Enum
        /// </summary>
        /// <typeparam name="T">The type of the Enum</typeparam>
        /// <param name="value">String value to parse</param>
        /// <returns>The Enum corresponding to the stringExtensions</returns>
        public static T EnumParse<T>(this string value)
        {
            return value.EnumParse<T>(false);
        }

        public static T EnumParse<T>(this string value, bool ignorecase)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            value = value.Trim();

            if (value.Length == 0)
            {
                throw new ArgumentException("Must specify valid information for parsing in the string.", "value");
            }

            var t = typeof (T);

            if (!t.IsEnum)
            {
                throw new ArgumentException("Type provided must be an Enum.", "T");
            }

            return (T) Enum.Parse(t, value, ignorecase);
        }

        [Conditional("NeverCompile")]
        private static void Test_EnumParse()
        {
            var foo = "Green".EnumParse<ConsoleColor>();
        }
    }
}