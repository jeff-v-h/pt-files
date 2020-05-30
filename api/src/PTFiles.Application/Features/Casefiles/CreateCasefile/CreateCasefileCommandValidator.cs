using FluentValidation;

namespace PTFiles.Application.Features.Casefiles.CreateCasefile
{
    public class CreateCasefileCommandValidator : AbstractValidator<CreateCasefileCommand>
    {
        public CreateCasefileCommandValidator()
        {
            RuleFor(v => v.PatientId)
                .NotEmpty().WithMessage("Patient id is required.");
              
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Casefile name is required.");
        }
    }
}
