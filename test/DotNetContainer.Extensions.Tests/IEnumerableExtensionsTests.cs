using System.Collections.Generic;
using Xunit;

namespace DotNetContainer.Extensions.Tests
{
    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void ConvertToEnumerable_GivenItem_ReturnsEnumerableWrappedItem()
        {
            var item = "Item";
            var result = item.ConvertToEnumerable();
            Assert.IsAssignableFrom<IEnumerable<string>>(result);
        }
    }
}
