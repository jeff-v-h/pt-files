using AutoMapper;
using FluentAssertions;
using PTFiles.Application.Common.Exceptions;
using PTFiles.Application.Features.Consultations.GetConsultation;
using PTFiles.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PTFiles.Application.UnitTests.Features.Consultations.GetConsultation
{
    [Collection("QueryTests")]
    public class GetConsultationQueryTests
    {
        private readonly PTFilesDbContext _context;
        private readonly IMapper _mapper;

        public GetConsultationQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVmAndCasefile()
        {
            var query = new GetConsultationQuery { Id = 1 };

            var handler = new GetConsultationQuery.GetConsultationQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<GetConsultationVm>();
            result.Id.Should().Be(1);
            result.Number.Should().Be(1);
            result.Plans.Should().Be("rv by end of week. ease into hydrotherapy when able");
        }

        [Fact]
        public async Task Handle_NoMatchingId_ThrowsNotFoundException()
        {
            var query = new GetConsultationQuery { Id = 99 };

            var handler = new GetConsultationQuery.GetConsultationQueryHandler(_context, _mapper);

            Func<Task> action = () => handler.Handle(query, CancellationToken.None);

            await action.Should().ThrowExactlyAsync<NotFoundException>();
        }
    }
}

