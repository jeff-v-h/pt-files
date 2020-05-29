using PTFiles.Application.Common.Models;

namespace PTFiles.Application.Features.Patients.GetPatient
{
    public class GetPatientVm : PersonVm
    {
        public string Occupation { get; set; }
    }
}
