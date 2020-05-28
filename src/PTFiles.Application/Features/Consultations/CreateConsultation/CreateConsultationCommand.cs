using AutoMapper;
using MediatR;
using PTFiles.Application.Common.Interfaces.Persistence;
using PTFiles.Application.Common.Models;
using PTFiles.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PTFiles.Application.Features.Consultations.CreateConsultation
{
    public class CreateConsultationCommand : IRequest<int>
    {
        public DateTime Date { get; set; }
        public int PractitionerId { get; set; }
        public SubjectiveAssessmentVm SubjectiveAssessment { get; set; }
        public ObjectiveAssessmentVm ObjectiveAssessment { get; set; }
        public string Treatments { get; set; }
        public string Plans { get; set; }

        public class CreateConsultationCommandHandler : IRequestHandler<CreateConsultationCommand, int>
        {
            private readonly IPTFilesDbContext _dbContext;
            private readonly IMapper _mapper;

            public CreateConsultationCommandHandler(IPTFilesDbContext context, IMapper mapper)
            {
                _dbContext = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateConsultationCommand command, CancellationToken cancelToken)
            {
                var consult = new Consultation
                {
                    Date = command.Date,
                    PractitionerId = command.PractitionerId,
                    SubjectiveAssessment = _mapper.Map<SubjectiveAssessment>(command.SubjectiveAssessment),
                    ObjectiveAssessment = _mapper.Map<ObjectiveAssessment>(command.ObjectiveAssessment),
                    Treatments = command.Treatments,
                    Plans = command.Plans
                };

                _dbContext.Consultations.Add(consult);
                await _dbContext.SaveChangesAsync(cancelToken);

                return consult.Id;
            }
        }
    }
}
