using System.Collections.Generic;

namespace PTFiles.Domain.Entities
{
    public class Practitioner : Person
    {
        public string JobLevel { get; set; }
        public string RegistrationID { get; set; }
        public List<Consultation> Consultations { get; set; }
    }
}
