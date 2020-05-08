using AutoMapper;
using PTFiles.Application.Common.Mappings;
using PTFiles.Persistence;
using System;
using Xunit;

namespace PTFiles.Application.UnitTests
{
    public sealed class QueryTestFixture : IDisposable
    {
        public PTFilesDbContext Context { get; }
        public IMapper Mapper { get; }

        public QueryTestFixture()
        {
            Context = PTFilesDbContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            PTFilesDbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryTests")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
