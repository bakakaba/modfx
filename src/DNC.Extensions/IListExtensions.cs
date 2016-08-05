using System.Collections.Generic;
using System.Linq;

namespace DNC.Extensions
{
    public static class IListExtensions
    {
        public static IList<T> ConvertToList<T>(this T item)
        {
            return item.ConvertToEnumerable().ToList();
        }
    }
}