using FluentValidation;

namespace PTFiles.Application.Features.Consultations.CreateConsultation
{
    public class CreateConsultationCommandValidator : AbstractValidator<CreateConsultationCommand>
    {
        public CreateConsultationCommandValidator()
        {
            RuleFor(v => v.Date)
                .NotEmpty().WithMessage("Date is required.");

            RuleFor(v => v.PractitionerId)
                .NotEmpty().WithMessage("Practitioner id is required.");

            RuleFor(v => v.CasefileId)
                .NotEmpty().WithMessage("Casefile id is required.");

            RuleFor(v => v.SubjectiveAssessment)
                .NotEmpty().WithMessage("SubjectiveAssessment property is required.");

            RuleFor(v => v.ObjectiveAssessment)
                .NotEmpty().WithMessage("ObjectiveAssessment property is required.");
        }
    }
}
