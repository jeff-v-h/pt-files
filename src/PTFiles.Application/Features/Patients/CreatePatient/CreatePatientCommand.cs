using MediatR;
using PTFiles.Application.Common.Interfaces.Persistence;
using PTFiles.Domain.Entities;
using PTFiles.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PTFiles.Application.Features.Patients.CreatePatient
{
    public class CreatePatientCommand : IRequest<int>
    {
        public Honorific Honorific { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public Gender Gender { get; set; }
        public string Occupation { get; set; }

        public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, int>
        {
            private readonly IPTFilesDbContext _dbContext;

            public CreatePatientCommandHandler(IPTFilesDbContext context)
            {
                _dbContext = context;
            }

            public async Task<int> Handle(CreatePatientCommand command, CancellationToken cancelToken)
            {
                var patient = new Patient
                {
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    Honorific = command.Honorific,
                    DOB = command.DOB,
                    Email = command.Email,
                    CountryCode = command.CountryCode,
                    HomePhone = command.HomePhone,
                    MobilePhone = command.MobilePhone,
                    Gender = command.Gender,
                    Occupation = command.Occupation
                };

                _dbContext.Patients.Add(patient);
                await _dbContext.SaveChangesAsync(cancelToken);

                return patient.Id;
            }
        }
    }
}
