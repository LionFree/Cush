using System.Collections.Generic;

namespace Cush.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T1, T2>(this ICollection<T1> list, params T2[] values) where T2 : T1
        {
            foreach (var value in values)
                list.Add(value);
        }

        //public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        //{
        //    // argument null checking omitted
        //    foreach (T item in sequence) action(item);
        //}
    }
}