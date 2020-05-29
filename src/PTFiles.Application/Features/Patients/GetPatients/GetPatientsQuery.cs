using AutoMapper;
using PTFiles.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using PTFiles.Application.Features.Patients.GetPatient;

namespace PTFiles.Application.Features.Patients.GetPatients
{
    public class GetPatientsQuery : IRequest<List<GetPatientVm>>
    {
        public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, List<GetPatientVm>>
        {
            private readonly IPTFilesDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetPatientsQueryHandler(IPTFilesDbContext context, IMapper mapper)
            {
                _dbContext = context;
                _mapper = mapper;
            }

            public async Task<List<GetPatientVm>> Handle(GetPatientsQuery query, CancellationToken token)
            {
                var patients = await _dbContext.Patients
                    .AsNoTracking()
                    .ToListAsync(token);

                return _mapper.Map<List<GetPatientVm>>(patients);
            }
        }
    }
}
