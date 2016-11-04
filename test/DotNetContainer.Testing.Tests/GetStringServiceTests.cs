using DotNetContainer.Testing.Tests.Resources;
using Xunit;

namespace DotNetContainer.Testing.Tests
{
    public class GetStringServiceTests : UnitTestFor<GetStringService>
    {
        [Fact]
        public void GetString_WhenDependencyMocked_DependencyInjected()
        {
            const string successfullyInjected = "Successfully injected.";
            Mock<IMockableInterface>()
                .Setup(x => x.GetString())
                .Returns(successfullyInjected);
            var result = Component.GetString();
            Assert.Equal(successfullyInjected, result);
        }

        [Fact]
        public void GetString_WhenDependencyNotMocked_LooseInjectionBehaviourObserved()
        {
            var result = Component.GetString();
            Assert.Equal(null, result);
        }
    }
}
