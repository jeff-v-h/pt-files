using System.Collections.Generic;

namespace PTFiles.Domain.Entities
{
    public class Patient : Person
    {
        public string Occupation { get; set; }
        public List<CaseFile> CaseFiles { get; set; }
    }
}
