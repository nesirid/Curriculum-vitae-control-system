using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PhoneNumber : BaseEntity
    {
        public string Number { get; set; }
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
