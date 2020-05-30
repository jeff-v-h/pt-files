using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PTFiles.Application.Common.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PTFiles.Application.Features.Patients.CreatePatient
{
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        private readonly IPTFilesDbContext _context;

        public CreatePatientCommandValidator(IPTFilesDbContext context)
        {
            _context = context;

            RuleFor(v => v.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("Title must not exceed 50 characters.");
            
            RuleFor(v => v.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last Name must not exceed 50 characters.");

            RuleFor(v => v.DOB)
                .NotEmpty().WithMessage("DOB is required.");           
           
            RuleFor(v => v.Email)
                .MaximumLength(255).WithMessage("Email must not exceed 255 characters.")
                .MustAsync(BeUniqueEmailIfExists).WithMessage("The specified email already exists.");
             
            RuleFor(v => v.HomePhone)
                .MaximumLength(40).WithMessage("Home phone must not exceed 40 characters.");            
            
            RuleFor(v => v.MobilePhone)
                .MaximumLength(40).WithMessage("Mobile phone must not exceed 40 characters.");
            
            RuleFor(v => v.Occupation)
                .MaximumLength(50).WithMessage("Occupation must not exceed 50 characters.");
        }

        public async Task<bool> BeUniqueEmailIfExists(string email, CancellationToken cancelToken)
        {
            if (email == null || email.Length == 0) return true;

            return await _context.Patients
                .AllAsync(l => l.Email != email);
        }
    }
}
