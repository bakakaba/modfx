using System.Collections.Generic;
using ModFx.Extensions;
using Xunit;

namespace ModFx.Core.Tests.Extensions
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
