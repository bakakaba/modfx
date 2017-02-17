using System.Collections.Generic;

namespace DotNetContainer.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ConvertToEnumerable<T>(this T item)
        {
            yield return item;
        }
    }
}