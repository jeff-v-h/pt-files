using AutoMapper;
using FluentAssertions;
using PTFiles.Application.Common.Exceptions;
using PTFiles.Application.Features.Patients.GetPatient;
using PTFiles.Domain.Enums;
using PTFiles.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PTFiles.Application.UnitTests.Features.Patients.GetPatient
{
    [Collection("QueryTests")]
    public class GetPatientQueryTests
    {
        private readonly PTFilesDbContext _context;
        private readonly IMapper _mapper;

        public GetPatientQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVmAndPatient()
        {
            var query = new GetPatientQuery { Id = 1 };

            var handler = new GetPatientQuery.GetPatientQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<GetPatientVm>();
            result.Id.Should().Be(1);
            result.Honorific.Should().Be(Honorific.Mr);
            result.FirstName.Should().Be("Jay");
        }

        [Fact]
        public async Task Handle_NoMatchingId_ThrowsNotFoundException()
        {
            var query = new GetPatientQuery { Id = 3 };

            var handler = new GetPatientQuery.GetPatientQueryHandler(_context, _mapper);

            Func<Task> action = () => handler.Handle(query, CancellationToken.None);

            await action.Should().ThrowExactlyAsync<NotFoundException>();
        }
    }
}
