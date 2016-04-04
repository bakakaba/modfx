using Xunit;
using DNC.Testing.Tests.Resources;

namespace DNC.Testing.Tests
{
    public class UnitTestsForTests : UnitTestsFor<GetStringService>
    {
        [Fact]
        public void TestGetString_WhenDependencyMocked_DependencyInjected()
        {
            const string SUCCESSFULLY_INJECTED = "Successfully injected.";
            Mock<IMockableInterface>()
                .Setup(x => x.GetString())
                .Returns(SUCCESSFULLY_INJECTED);
            var result = Component.GetString();
            Assert.Equal(SUCCESSFULLY_INJECTED, result);
        }
        
        [Fact]
        public void TestGetString_WhenDependencyNotMocked_LooseInjectionBehaviourObserved()
        {
            var result = Component.GetString();
            Assert.Equal(null, result);
        }
    }
}