﻿using Domain.Common;


namespace Domain.Entities
{
    public class PhoneNumber : BaseEntity
    {
        public string Number { get; set; }

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
