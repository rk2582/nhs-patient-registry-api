using FluentValidation;
namespace Nhs.PatientRegistry.Api.Validation
{

    /// <summary>
    /// Checks if a Patient ID is valid.
    /// It ensures the number is 1 or higher.
    /// </summary>
    public class PatientIdValidator : AbstractValidator<int>
    {
        public PatientIdValidator()
        {
            RuleFor(id => id)
                .GreaterThan(0)
                .WithMessage("Patient ID must be greater than zero.");
        }
    }
}
