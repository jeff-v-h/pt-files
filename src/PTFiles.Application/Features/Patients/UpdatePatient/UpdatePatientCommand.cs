using AutoMapper;
using MediatR;
using PTFiles.Application.Common.Extensions;
using PTFiles.Application.Common.Interfaces.Persistence;
using PTFiles.Domain.Entities;
using PTFiles.Domain.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PTFiles.Application.Features.Patients.UpdatePatient
{
    public class UpdatePatientCommand : IRequest
    {
        public int Id { get; set; }
        public Honorific Honorific { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public Gender Gender { get; set; }


        public class UpdatepatientCommandHandler : IRequestHandler<UpdatePatientCommand>
        {
            private readonly IPTFilesDbContext _dbContext;
            private readonly IMapper _mapper;

            public UpdatepatientCommandHandler(IPTFilesDbContext context, IMapper mapper)
            {
                _dbContext = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdatePatientCommand command, CancellationToken cancelToken)
            {
                var patient = await _dbContext.Patients
                    .Where(p => p.Id == command.Id)
                    .FirstOrNotFoundAsync(nameof(Patient), command.Id, cancelToken);

                patient.FirstName = command.FirstName;
                patient.LastName = command.LastName;
                patient.Honorific = command.Honorific;
                patient.DOB = command.DOB;
                patient.Email = command.Email;
                patient.CountryCode = command.CountryCode;
                patient.HomePhone = command.HomePhone;
                patient.MobilePhone = command.MobilePhone;
                patient.Gender = command.Gender;

                _dbContext.Patients.Update(patient);
                await _dbContext.SaveChangesAsync(cancelToken);

                return Unit.Value;
            }
        }
    }
}
