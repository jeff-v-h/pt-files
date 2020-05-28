using FluentValidation;

namespace PTFiles.Application.Features.Consultations.UpdateConsultation
{
    public class UpdateConsultationCommandValidator : AbstractValidator<UpdateConsultationCommand>
    {
        public UpdateConsultationCommandValidator()
        {
            RuleFor(v => v.Date)
                .NotEmpty().WithMessage("Date is required.");
        }
    }
}
