using AutoMapper;
using PTFiles.Application.Common.Extensions;
using PTFiles.Application.Common.Interfaces.Persistence;
using PTFiles.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PTFiles.Application.Features.SubjectiveAx.GetSubjectiveAssessment
{
    public class GetSubjectiveAssessmentQuery : IRequest<GetSubjectiveAssessmentVm>
    {
        public int ConsultationId { get; set; }

        public class GetSubjectiveAssessmentQueryHandler : IRequestHandler<GetSubjectiveAssessmentQuery, GetSubjectiveAssessmentVm>
        {
            private readonly IPTFilesDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetSubjectiveAssessmentQueryHandler(IPTFilesDbContext context, IMapper mapper)
            {
                _dbContext = context;
                _mapper = mapper;
            }

            public async Task<GetSubjectiveAssessmentVm> Handle(GetSubjectiveAssessmentQuery query, CancellationToken token)
            {
                var subjective = await _dbContext.SubjectiveAssessments.AsNoTracking()
                    .Where(s => s.ConsultationId == query.ConsultationId)
                    .FirstOrNotFoundAsync(nameof(SubjectiveAssessment), $"ConsultationId {query.ConsultationId}", token);

                return _mapper.Map<GetSubjectiveAssessmentVm>(subjective);
            }
        }
    }
}
