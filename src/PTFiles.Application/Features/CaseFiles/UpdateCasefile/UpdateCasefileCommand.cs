using MediatR;
using PTFiles.Application.Common.Exceptions;
using PTFiles.Application.Common.Extensions;
using PTFiles.Application.Common.Interfaces.Persistence;
using PTFiles.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PTFiles.Application.Features.Casefiles.UpdateCasefile
{
    public class UpdateCasefileCommand : IRequest
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; }

        public class UpdatePatientCommandHandler : IRequestHandler<UpdateCasefileCommand>
        {
            private readonly IPTFilesDbContext _dbContext;

            public UpdatePatientCommandHandler(IPTFilesDbContext context)
            {
                _dbContext = context;
            }

            public async Task<Unit> Handle(UpdateCasefileCommand command, CancellationToken cancelToken)
            {
                var casefile = await _dbContext.Casefiles
                    .Where(p => p.Id == command.Id)
                    .FirstOrNotFoundAsync(nameof(Patient), command.Id, cancelToken);

                if (casefile == null)
                {
                    throw new NotFoundException(nameof(Patient), command.Id);
                }

                casefile.PatientId = command.PatientId;
                casefile.Name = command.Name;

                _dbContext.Casefiles.Update(casefile);
                await _dbContext.SaveChangesAsync(cancelToken);

                return Unit.Value;
            }
        }
    }
}
