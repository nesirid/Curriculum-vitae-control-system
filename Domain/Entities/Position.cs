using Domain.Common;
using System;
namespace Domain.Entities
{
    public class Position : BaseEntity
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CandidateCompanyId { get; set; }
        public CandidateCompany CandidateCompany { get; set; }

    }
}
