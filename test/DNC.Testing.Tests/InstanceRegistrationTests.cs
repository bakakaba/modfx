using Xunit;
using DNC.Testing.Tests.Resources;

namespace DNC.Testing.Tests
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
