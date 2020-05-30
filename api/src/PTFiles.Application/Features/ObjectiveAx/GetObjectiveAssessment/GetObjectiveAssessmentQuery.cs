using AutoMapper;
using PTFiles.Application.Common.Extensions;
using PTFiles.Application.Common.Interfaces.Persistence;
using PTFiles.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PTFiles.Application.Features.ObjectiveAx.GetObjectiveAssessment
{
    public class GetObjectiveAssessmentQuery : IRequest<GetObjectiveAssessmentVm>
    {
        public int ConsultationId { get; set; }

        public class GetObjectiveAssessmentQueryHandler : IRequestHandler<GetObjectiveAssessmentQuery, GetObjectiveAssessmentVm>
        {
            private readonly IPTFilesDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetObjectiveAssessmentQueryHandler(IPTFilesDbContext context, IMapper mapper)
            {
                _dbContext = context;
                _mapper = mapper;
            }

            public async Task<GetObjectiveAssessmentVm> Handle(GetObjectiveAssessmentQuery query, CancellationToken token)
            {
                var objective = await _dbContext.ObjectiveAssessments.AsNoTracking()
                    .Where(o => o.ConsultationId == query.ConsultationId)
                    .FirstOrNotFoundAsync(nameof(ObjectiveAssessment), $"ConsultationId {query.ConsultationId}", token);

                return _mapper.Map<GetObjectiveAssessmentVm>(objective);
            }
        }
    }
}
