using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Candidate : BaseEntity
    {
        public string FullName { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; } = new();
        public string Description { get; set; }
        public DateTime Birthday { get; set; }  
        public string BirthPlace { get; set; }
        public GenderType Gender { get; set; }  
        public string MaritalStatus { get; set; }
        public DriverLicenseType DriverLicense { get; set; }
        public string Education { get; set; }
        public List<Experience> WorkHistory { get; set; } = new();
        public List<string> Skills { get; set; } = new();
        public List<string> Languages { get; set; } = new();
        public List<string> Certificates { get; set; } = new();

        public List<CandidateCompany> CandidateCompanies { get; set; } = new();
    }
}
