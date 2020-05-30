using AutoMapper;
using PTFiles.Application.Common.Extensions;
using PTFiles.Application.Common.Interfaces.Persistence;
using PTFiles.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PTFiles.Application.Features.Patients.GetPatient
{
    public class GetPatientQuery : IRequest<GetPatientVm>
    {
        public int Id { get; set; }

        public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, GetPatientVm>
        {
            private readonly IPTFilesDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetPatientQueryHandler(IPTFilesDbContext context, IMapper mapper)
            {
                _dbContext = context;
                _mapper = mapper;
            }

            public async Task<GetPatientVm> Handle(GetPatientQuery query, CancellationToken token)
            {
                var patient = await _dbContext.Patients
                    .AsNoTracking()
                    .Include(p => p.Casefiles)
                    .Where(p => p.Id == query.Id)
                    .FirstOrNotFoundAsync(nameof(Patient), query.Id, token);

                return _mapper.Map<GetPatientVm>(patient);
            }
        }
    }
}
