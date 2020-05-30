using MediatR;
using PTFiles.Application.Common.Exceptions;
using PTFiles.Application.Common.Extensions;
using PTFiles.Application.Common.Interfaces.Persistence;
using PTFiles.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PTFiles.Application.Features.Casefiles.DeleteCasefile
{
    public class DeleteCasefileCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteTodoListCommandHandler : IRequestHandler<DeleteCasefileCommand>
        {
            private readonly IPTFilesDbContext _dbContext;

            public DeleteTodoListCommandHandler(IPTFilesDbContext context)
            {
                _dbContext = context;
            }

            public async Task<Unit> Handle(DeleteCasefileCommand command, CancellationToken cancelToken)
            {
                var casefile = await _dbContext.Casefiles
                    .Where(p => p.Id == command.Id)
                    .FirstOrNotFoundAsync(nameof(Casefile), command.Id, cancelToken);

                if (casefile == null)
                {
                    throw new NotFoundException(nameof(Casefile), command.Id);
                }

                _dbContext.Casefiles.Remove(casefile);

                await _dbContext.SaveChangesAsync(cancelToken);

                return Unit.Value;
            }
        }
    }
}
