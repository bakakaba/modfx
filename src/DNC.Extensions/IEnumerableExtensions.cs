using System.Collections.Generic;

namespace DNC.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }
    }
}