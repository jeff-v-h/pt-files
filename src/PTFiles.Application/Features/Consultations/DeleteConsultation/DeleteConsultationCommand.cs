using MediatR;
using PTFiles.Application.Common.Exceptions;
using PTFiles.Application.Common.Extensions;
using PTFiles.Application.Common.Interfaces.Persistence;
using PTFiles.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PTFiles.Application.Features.Consultations.DeleteConsultation
{
    public class DeleteConsultationCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteTodoListCommandHandler : IRequestHandler<DeleteConsultationCommand>
        {
            private readonly IPTFilesDbContext _dbContext;

            public DeleteTodoListCommandHandler(IPTFilesDbContext context)
            {
                _dbContext = context;
            }

            public async Task<Unit> Handle(DeleteConsultationCommand command, CancellationToken cancelToken)
            {
                var consult = await _dbContext.Consultations
                    .Where(p => p.Id == command.Id)
                    .FirstOrNotFoundAsync(nameof(Consultation), command.Id, cancelToken);

                if (consult == null)
                {
                    throw new NotFoundException(nameof(Consultation), command.Id);
                }

                _dbContext.Consultations.Remove(consult);

                await _dbContext.SaveChangesAsync(cancelToken);

                return Unit.Value;
            }
        }
    }
}
