using AutoMapper;
using PTFiles.Application.Features.Casefiles.GetCasefile;
using PTFiles.Application.Features.Consultations.GetConsultation;
using PTFiles.Application.Features.Patients.GetPatient;
using PTFiles.Application.Features.Patients.GetPatients;
using PTFiles.Application.Features.SubjectiveAx.GetSubjectiveAssessment;
using PTFiles.Domain.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace PTFiles.Application.UnitTests.Common.Mappings
{
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(Patient), typeof(GetPatientVm))]
        [InlineData(typeof(Casefile), typeof(GetCasefileVm))]
        [InlineData(typeof(Consultation), typeof(GetConsultationVm))]
        [InlineData(typeof(SubjectiveAssessment), typeof(GetSubjectiveAssessmentVm))]
        [InlineData(typeof(GetSubjectiveAssessmentVm), typeof(SubjectiveAssessment))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }
    }
}
