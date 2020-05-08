using AutoMapper;
using PTFiles.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace PTFiles.Application.Features.Patients.GetPatients
{
    public class GetPatientsQuery : IRequest<GetPatientsVm>
    {
        public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, GetPatientsVm>
        {
            private readonly IPTFilesDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetPatientsQueryHandler(IPTFilesDbContext context, IMapper mapper)
            {
                _dbContext = context;
                _mapper = mapper;
            }

            public async Task<GetPatientsVm> Handle(GetPatientsQuery query, CancellationToken token)
            {
                var patients = await _dbContext.Patients
                    .AsNoTracking()
                    .Include(p => p.CaseFiles)
                    .ToListAsync(token);

                return _mapper.Map<GetPatientsVm>(patients);
            }
        }
    }
}
