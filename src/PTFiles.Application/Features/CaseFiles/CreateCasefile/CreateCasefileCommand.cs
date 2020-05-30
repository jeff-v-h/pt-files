using MediatR;
using PTFiles.Application.Common.Interfaces.Persistence;
using PTFiles.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PTFiles.Application.Features.Casefiles.CreateCasefile
{
    public class CreateCasefileCommand : IRequest<int>
    {
        public int PatientId { get; set; }
        public string Name { get; set; }

        public class CreateCasefileCommandHandler : IRequestHandler<CreateCasefileCommand, int>
        {
            private readonly IPTFilesDbContext _dbContext;

            public CreateCasefileCommandHandler(IPTFilesDbContext context)
            {
                _dbContext = context;
            }

            public async Task<int> Handle(CreateCasefileCommand command, CancellationToken cancelToken)
            {
                var casefile = new Casefile
                {
                    PatientId = command.PatientId,
                    Name = command.Name,
                    Created = DateTime.UtcNow
                };

                _dbContext.Casefiles.Add(casefile);
                await _dbContext.SaveChangesAsync(cancelToken);

                return casefile.Id;
            }
        }
    }
}
