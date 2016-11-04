using System.Collections.Generic;

namespace DotNetContainer.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ConvertToEnumerable<T>(this T item)
        {
            yield return item;
        }
    }
}