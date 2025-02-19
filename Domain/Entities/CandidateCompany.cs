
namespace Domain.Entities
{
    public class CandidateCompany
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set;}

        public List<Position> Positions { get; set; } = new();
    }
}
