using ModFx.Testing.Tests.Resources;
using Xunit;

namespace ModFx.Testing.Tests
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
