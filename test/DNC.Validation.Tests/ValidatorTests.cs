using System;
using System.ComponentModel.DataAnnotations;
using DNC.Validation.Tests.Resources;
using Xunit;

namespace DNC.Validation.Tests
{
    public class ValidatorTests
    {
        [Fact]
        public void Validate_EvaluateObject_EvaluatesCorrectly()
        {
            Require.That(null as object).Validate(x => x == null, "x == null");
            Assert.Throws<ArgumentException>(
                () => Require.That(null as object).Validate(x => x != null, "x != null"));
        }

        [Fact]
        public void Validate_EvaluateString_EvaluatesCorrectly()
        {
            Require.That(string.Empty).Validate(x => x == string.Empty, "x == string.Empty");
            Assert.Throws<ArgumentException>(
                () => Require.That(string.Empty).Validate(x => x != string.Empty, "x != string.Empty"));
        }

        [Fact]
        public void Validate_EvaluateIntRange_EvaluatesCorrectly()
        {
            Require.That(1).Validate(x => x > 0, "x > 0");
            Assert.Throws<ArgumentException>(() => Require.That(1).Validate(x => x < 0, "x < 0"));
        }

        [Fact]
        public void Validate_RequirementsNotMet_ThrowsSpecifiedException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => Require.That(0).Validate<ArgumentOutOfRangeException>(x => x != 0, "x != 0"));
        }

        [Fact]
        public void IsValidModel_IsValid_Passes()
        {
            var model = new TestModel
            {
                Content = "Test",
                TestRegexContent = "TestRegex"
            };

            Require.That(model).IsValidModel();
        }

        [Fact]
        public void IsValidModel_FailRegex_Throws()
        {
            var model = new TestModel
            {
                Content = "Test",
                TestRegexContent = "FailRegex"
            };

            Assert.Throws<ValidationException>(() => Require.That(model).IsValidModel());
        }

        [Fact]
        public void IsValidProperty_IsValid_Passes()
        {
            var model = new TestModel { Content = "Test" };
            Require.That(model).IsValidProperty(x => x.Content);
        }

        [Fact]
        public void IsValidProperty_FailRegex_Throws()
        {
            var model = new TestModel { TestRegexContent = "FailRegex" };
            Assert.Throws<ValidationException>(() => Require.That(model).IsValidProperty(x => x.TestRegexContent));
        }
    }
}