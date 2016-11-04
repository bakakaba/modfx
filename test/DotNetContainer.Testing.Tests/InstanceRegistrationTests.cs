using DotNetContainer.Testing.Tests.Resources;
using Xunit;

namespace DotNetContainer.Testing.Tests
{
    public class InstanceRegistrationTests : UnitTestFor<TestModel>
    {
        [Fact]
        public void RegisterInstance_WhenInstanceRegistered_InstanceIsUsed()
        {
            var instance = new TestModel { Content = "Test content." };
            RegisterInstance(instance);
            Assert.Equal(instance, Component);
        }
    }
}
