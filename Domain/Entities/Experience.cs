using Domain.Common;

namespace Domain.Entities
{
    public class Experience : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
