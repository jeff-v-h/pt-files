using PTFiles.Persistence;
using System;

namespace PTFiles.Application.UnitTests
{
    public class CommandTestBase : IDisposable
    {
        public PTFilesDbContext Context { get; }

        public CommandTestBase()
        {
            Context = PTFilesDbContextFactory.Create();
        }

        public void Dispose()
        {
            PTFilesDbContextFactory.Destroy(Context);
        }
    }
}
