using FluentValidation;


namespace PTFiles.Application.Features.Casefiles.UpdateCasefile
{
    public class UpdateCasefileCommandValidator : AbstractValidator<UpdateCasefileCommand>
    {
        public UpdateCasefileCommandValidator()
        {
            RuleFor(v => v.PatientId)
                .NotEmpty().WithMessage("Patient id is required.");

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name for casefile is required.");
        }
    }
}
