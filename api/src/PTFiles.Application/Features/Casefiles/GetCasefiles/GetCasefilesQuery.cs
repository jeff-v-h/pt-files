using AutoMapper;
using PTFiles.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using PTFiles.Application.Features.Casefiles.GetCasefile;
using System.Collections.Generic;
using System.Linq;

namespace PTFiles.Application.Features.Casefiles.GetCasefiles
{
    public class GetCasefilesQuery : IRequest<List<GetCasefileVm>>
    {
        public int patientId { get; set; }

        public class GetCasefilesQueryHandler : IRequestHandler<GetCasefilesQuery, List<GetCasefileVm>>
        {
            private readonly IPTFilesDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetCasefilesQueryHandler(IPTFilesDbContext context, IMapper mapper)
            {
                _dbContext = context;
                _mapper = mapper;
            }

            public async Task<List<GetCasefileVm>> Handle(GetCasefilesQuery query, CancellationToken token)
            {
                var casefiles = await _dbContext.Casefiles
                    .AsNoTracking()
                    .Where(c => c.PatientId == query.patientId)
                    .ToListAsync(token);

                return _mapper.Map<List<GetCasefileVm>>(casefiles);
            }
        }
    }
}
