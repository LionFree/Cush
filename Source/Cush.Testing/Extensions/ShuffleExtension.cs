using System.Collections.Generic;

namespace Cush.Testing
{
    public static class ShuffleExtension
    {
        /// <summary>
        ///     Shuffles a <see cref="T:List" /> or <see cref="T:IList" />.
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = GetRandom.Int(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        ///     Shuffles an array.
        /// </summary>
        public static void Shuffle<T>(this T[] list)
        {
            var n = list.Length;
            while (n > 1)
            {
                n--;
                var k = GetRandom.Int(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}