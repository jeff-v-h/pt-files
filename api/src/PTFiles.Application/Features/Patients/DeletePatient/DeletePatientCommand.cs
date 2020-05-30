using MediatR;
using PTFiles.Application.Common.Exceptions;
using PTFiles.Application.Common.Extensions;
using PTFiles.Application.Common.Interfaces.Persistence;
using PTFiles.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PTFiles.Application.Features.Patients.DeletePatient
{
    public class DeletePatientCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteTodoListCommandHandler : IRequestHandler<DeletePatientCommand>
        {
            private readonly IPTFilesDbContext _dbContext;

            public DeleteTodoListCommandHandler(IPTFilesDbContext context)
            {
                _dbContext = context;
            }

            public async Task<Unit> Handle(DeletePatientCommand command, CancellationToken cancelToken)
            {
                var patient = await _dbContext.Patients
                    .Where(p => p.Id == command.Id)
                    .FirstOrNotFoundAsync(nameof(Patient), command.Id, cancelToken);

                if (patient == null)
                {
                    throw new NotFoundException(nameof(Patient), command.Id);
                }

                _dbContext.Patients.Remove(patient);

                await _dbContext.SaveChangesAsync(cancelToken);

                return Unit.Value;
            }
        }
    }
}
