using System;
using DNC.Validation.Tests.Resources;
using DNC.Validation.Validators;
using Xunit;

namespace DNC.Validation.Tests.Validators
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
    }
}