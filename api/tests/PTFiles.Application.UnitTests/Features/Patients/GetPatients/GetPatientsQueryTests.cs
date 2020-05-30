using AutoMapper;
using FluentAssertions;
using PTFiles.Application.Features.Patients.GetPatient;
using PTFiles.Application.Features.Patients.GetPatients;
using PTFiles.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PTFiles.Application.UnitTests.Features.Patients.GetPatients
{
    [Collection("QueryTests")]
    public class GetPatientsQueryTests
    {
        private readonly PTFilesDbContext _context;
        private readonly IMapper _mapper;

        public GetPatientsQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVm()
        {
            var query = new GetPatientsQuery();

            var handler = new GetPatientsQuery.GetPatientsQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<List<GetPatientVm>>();
            result.Count.Should().Be(2);
        }
    }
}
