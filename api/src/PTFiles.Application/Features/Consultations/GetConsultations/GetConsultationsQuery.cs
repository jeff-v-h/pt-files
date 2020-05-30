using AutoMapper;
using PTFiles.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using PTFiles.Application.Features.Consultations.GetConsultation;

namespace PTFiles.Application.Features.Consultations.GetConsultations
{
    public class GetConsultationsQuery : IRequest<List<GetConsultationBaseVm>>
    {
        public int CasefileId { get; set; }

        public class GetCasefilesQueryHandler : IRequestHandler<GetConsultationsQuery, List<GetConsultationBaseVm>>
        {
            private readonly IPTFilesDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetCasefilesQueryHandler(IPTFilesDbContext context, IMapper mapper)
            {
                _dbContext = context;
                _mapper = mapper;
            }

            public async Task<List<GetConsultationBaseVm>> Handle(GetConsultationsQuery query, CancellationToken token)
            {
                var consults = await _dbContext.Consultations
                    .AsNoTracking()
                    .Where(c => c.CasefileId == query.CasefileId)
                    .ToListAsync(token);

                return _mapper.Map<List<GetConsultationBaseVm>>(consults);
            }
        }
    }
}
