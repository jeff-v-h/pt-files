using AutoMapper;
using PTFiles.Application.Common.Extensions;
using PTFiles.Application.Common.Interfaces.Persistence;
using PTFiles.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PTFiles.Application.Features.Casefiles.GetCasefile
{
    public class GetCasefileQuery : IRequest<GetCasefileVm>
    {
        public int Id { get; set; }

        public class GetCasefileQueryHandler : IRequestHandler<GetCasefileQuery, GetCasefileVm>
        {
            private readonly IPTFilesDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetCasefileQueryHandler(IPTFilesDbContext context, IMapper mapper)
            {
                _dbContext = context;
                _mapper = mapper;
            }

            public async Task<GetCasefileVm> Handle(GetCasefileQuery query, CancellationToken token)
            {
                var file = await _dbContext.Casefiles
                    .AsNoTracking()
                    //.Include(c => c.Consultations)
                    //.Include(c => c.Patient)
                    .Where(p => p.Id == query.Id)
                    .FirstOrNotFoundAsync(nameof(Casefile), query.Id, token);

                return _mapper.Map<GetCasefileVm>(file);
            }
        }
    }
}
