using Domain.Common;

namespace Domain.Entities
{
    public class CandidatePhoto : BaseEntity
    {
        public string Url { get; set; } 
        public bool IsMain { get; set; } = false; 

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
