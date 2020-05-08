using AutoMapper;
using FluentAssertions;
using PTFiles.Application.Common.Exceptions;
using PTFiles.Application.Features.CaseFiles.GetCaseFile;
using PTFiles.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PTFiles.Application.UnitTests.Features.CaseFiles.GetCaseFile
{
    [Collection("QueryTests")]
    public class GetCaseFileQueryTests
    {
        private readonly PTFilesDbContext _context;
        private readonly IMapper _mapper;

        public GetCaseFileQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVmAndCaseFile()
        {
            var query = new GetCaseFileQuery { Id = 1 };

            var handler = new GetCaseFileQuery.GetCaseFileQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<GetCaseFileVm>();
            result.Id.Should().Be(1);
            result.Name.Should().Be("Lower Back Injury");
        }

        [Fact]
        public async Task Handle_NoMatchingId_ThrowsNotFoundException()
        {
            var query = new GetCaseFileQuery { Id = 99 };

            var handler = new GetCaseFileQuery.GetCaseFileQueryHandler(_context, _mapper);

            Func<Task> action = () => handler.Handle(query, CancellationToken.None);

            await action.Should().ThrowExactlyAsync<NotFoundException>();
        }
    }
}

