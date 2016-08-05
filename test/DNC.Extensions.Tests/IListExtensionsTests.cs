using System.Collections.Generic;
using Xunit;

namespace DNC.Extensions.Tests
{
    public class IListExtensionsTests
    {
        [Fact]
        public void ConvertToList_GivenItem_ReturnsEnumerableWrappedItem()
        {
            var item = "Item";
            var result = item.ConvertToList();
            Assert.IsAssignableFrom<IList<string>>(result);
        }
    }
}