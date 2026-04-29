using Nhs.PatientRegistry.Api.Validation;
using FluentAssertions;

namespace Nhs.PatientRegistry.Tests.Validation
{
    public class PatientIdValidatorTests
    {
        private readonly PatientIdValidator _validator = new();

        [Fact]
        public void Validate_WhenPatientIdIsGreaterThanZero_ReturnsValid()
        {
            var result = _validator.Validate(1);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WhenPatientIdIsZero_ReturnsInvalid()
        {
            var result = _validator.Validate(0);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(error =>
                error.ErrorMessage == "Patient ID must be greater than zero.");
        }

        [Fact]
        public void Validate_WhenPatientIdIsNegative_ReturnsInvalid()
        {
            var result = _validator.Validate(-1);

            result.IsValid.Should().BeFalse();
        }
    }
}
