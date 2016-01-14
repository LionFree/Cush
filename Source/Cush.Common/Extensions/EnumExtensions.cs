using System;
using System.Linq;

namespace Cush.Common
{
    public static class EnumExtensions
    {
        public static TEnum Min<TEnum>(this TEnum enumeration) where TEnum : struct, IComparable, IFormattable
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("TEnum must be an enumerated type.");
            return Enumerable.Min(Enum.GetValues(typeof(TEnum)).Cast<TEnum>());
        }

        public static TEnum Max<TEnum>(this TEnum enumeration) where TEnum : struct, IComparable, IFormattable
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("TEnum must be an enumerated type.");
            return Enumerable.Max(Enum.GetValues(typeof(TEnum)).Cast<TEnum>());
        }
    }
}