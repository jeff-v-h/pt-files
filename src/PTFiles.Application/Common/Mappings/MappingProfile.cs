using AutoMapper;
using PTFiles.Application.Common.Models;
using PTFiles.Application.Features.Casefiles.GetCasefile;
using PTFiles.Application.Features.Consultations.GetConsultation;
using PTFiles.Application.Features.ObjectiveAx.GetObjectiveAssessment;
using PTFiles.Application.Features.Patients.GetPatient;
using PTFiles.Application.Features.Patients.GetPatients;
using PTFiles.Application.Features.SubjectiveAx.GetSubjectiveAssessment;
using PTFiles.Domain.Entities;
using System.Collections.Generic;

namespace PTFiles.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, GetPatientVm>();

            CreateMap<Casefile, GetCasefileVm>();
            CreateMap<Consultation, ConsultVm>();
            CreateMap<Patient, FilesPatientVm>();

            CreateMap<Consultation, GetConsultationVm>();
            CreateMap<Consultation, GetConsultationBaseVm>();
            CreateMap<Practitioner, PractitionerVm>();
            CreateMap<SubjectiveAssessment, SubjectiveAssessmentVm>();
            CreateMap<SubjectiveAssessmentVm, SubjectiveAssessment>()
                .ForMember(d => d.Consultation, opt => opt.Ignore());
            CreateMap<ObjectiveAssessment, ObjectiveAssessmentVm>();
            CreateMap<ObjectiveAssessmentVm, ObjectiveAssessment>()
                .ForMember(d => d.Consultation, opt => opt.Ignore());

            CreateMap<SubjectiveAssessment, GetSubjectiveAssessmentVm>();

            CreateMap<ObjectiveAssessment, GetObjectiveAssessmentVm>();
        }
    }
}
