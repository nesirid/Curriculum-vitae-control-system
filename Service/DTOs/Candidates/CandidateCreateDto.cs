using Domain.Enums;

namespace Service.DTOs.Candidates
{
    public class CandidateCreateDto
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public DateTime Birthday { get; set; }
        public string BirthPlace { get; set; }

        public GenderType Gender { get; set; }
        public DriverLicenseType DriverLicense { get; set; }

        public string MaritalStatus { get; set; }
        public string Education { get; set; }

        public List<string> Skills { get; set; } = new();
        public List<string> Languages { get; set; } = new();
        public List<string> Certificates { get; set; } = new();
        public List<string> PhoneNumbers { get; set; } = new();
    }
}
