using System.Collections.Generic;
using Xunit;

namespace DNC.Extensions.Tests
{
    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void Yield_GivenItem_ReturnsEnumerableWrappedItem()
        {
            var item = "Item";
            var result = item.Yield();
            Assert.IsAssignableFrom<IEnumerable<string>>(result);
        }
    }
}