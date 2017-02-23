using System;
using System.Collections.Generic;

namespace ModFx.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ConvertToEnumerable<T>(this T item)
        {
            yield return item;
        }

        public static void ForEach<T>(this IEnumerable<T> @enum, Action<T> action)
        {
            foreach (var item in @enum)
                action(item);
        }
    }
}