using System.Collections.Generic;

namespace ModFx.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ConvertToEnumerable<T>(this T item)
        {
            yield return item;
        }
    }
}