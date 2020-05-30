using System;

namespace PTFiles.Application.Features.Casefiles.GetCasefile
{
    public class GetCasefileVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public int PatientId { get; set; }
    }
}
