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
        public int CasefileId { get; set; }
        public CreateConsultationSubjective SubjectiveAssessment { get; set; }
        public CreateConsultationObjective ObjectiveAssessment { get; set; }
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
                // Create consult first to get an Id
                var consult = new Consultation
                {
                    Date = command.Date,
                    PractitionerId = command.PractitionerId,
                    CasefileId = command.CasefileId,
                    Treatments = command.Treatments,
                    Plans = command.Plans
                };

                _dbContext.Consultations.Add(consult);
                await _dbContext.SaveChangesAsync(cancelToken);

                // Pass consultId to subjective and objective to save and get their own IDs
                var subjective = _mapper.Map<SubjectiveAssessment>(command.SubjectiveAssessment);
                subjective.ConsultationId = consult.Id;
                var objective = _mapper.Map<ObjectiveAssessment>(command.ObjectiveAssessment);
                objective.ConsultationId = consult.Id;

                _dbContext.SubjectiveAssessments.Add(subjective);
                _dbContext.ObjectiveAssessments.Add(objective);

                await _dbContext.SaveChangesAsync(cancelToken);

                // Pass subjective and objective ids back into consult and update it
                consult.SubjectiveId = subjective.Id;
                consult.ObjectiveId = objective.Id;

                _dbContext.Consultations.Update(consult);
                await _dbContext.SaveChangesAsync(cancelToken);

                return consult.Id;
            }
        }
    }
}
