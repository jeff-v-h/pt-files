using PTFiles.Application.Common.Models;
using System;

namespace PTFiles.Application.Features.Consultations.GetConsultation
{
    public class GetConsultationVm : GetConsultationBaseVm
    {
        public PractitionerVm Practitioner { get; set; }
        public SubjectiveAssessmentVm SubjectiveAssessment { get; set; }
        public ObjectiveAssessmentVm ObjectiveAssessment { get; set; }
        public string Treatments { get; set; }
        public string Plans { get; set; }
    }

    public class GetConsultationBaseVm
    {
        public int Id { get; set; }
        public int CasefileId { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }
    }
}
