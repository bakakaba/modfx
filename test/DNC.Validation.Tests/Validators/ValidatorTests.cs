using System;
using Xunit;

namespace DNC.Validation.Tests.Validators
{
    public class ObjectValidatorTests
    {
        [Fact]
        public void IsNull_EvaluatesCorrect()
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
    }
}