using System;
using DotNetContainer.Validation.Tests.Resources;
using DotNetContainer.Validation.Validators;
using Xunit;

namespace DotNetContainer.Validation.Tests.Validators
{
    public class ObjectValidatorTests
    {
        [Fact]
        public void IsNotNull_WhenNull_ThrowsArgumentException()
        {
            TestModel model = null;
            Assert.Throws<ArgumentException>(
                () => Require.That(model).IsNotNull());
        }

        [Fact]
        public void IsNotNull_WhenNull_ThrowsArgumentNullException()
        {
            TestModel model = null;
            Assert.Throws<ArgumentNullException>(
                () => Require.That(model).IsNotNull<TestModel, ArgumentNullException>());
        }

        [Theory]
        [InlineData("string")]
        [InlineData(0)]
        [InlineData(true)]
        public void IsDefault_NotDefaultString_ThrowsArgumentException(object item)
        {
            Assert.Throws<ArgumentException>(
                () => Require.That(item).IsDefault());
        }
    }
}