using System.Collections.Generic;
using ModFx.Extensions;
using Xunit;

namespace ModFx.Core.Tests.Extensions
{
    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void ConvertToEnumerable_GivenItem_ReturnsEnumerableWrappedItem()
        {
            const string item = "Item";
            var result = item.ConvertToEnumerable();
            Assert.IsAssignableFrom<IEnumerable<string>>(result);
        }
    }
}
