using PTFiles.Application.Common.Models;
using System.Collections.Generic;

namespace PTFiles.Application.Features.Patients.GetPatient
{
    public class GetPatientVm : PersonVm
    {
        public string Occupation { get; set; }
        public IReadOnlyCollection<PatientCasefileVm> Casefiles { get; set; }
    }

    public class PatientCasefileVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
