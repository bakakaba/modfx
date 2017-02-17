using System.Collections.Generic;
using DotNetContainer.Core.Extensions;
using Xunit;

namespace DotNetContainer.Core.Tests.Extensions
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
