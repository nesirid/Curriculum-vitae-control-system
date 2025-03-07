﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Candidates
{
    public class CandidateExtractedDto
    {
        public string CacheKey { get; set; } 
        public string FullName { get; set; }
        public List<string> PhoneNumbers { get; set; } = new();
        public string Description { get; set; }
        public DateTime? Birthday { get; set; }
        public string BirthPlace { get; set; }
        public GenderType? Gender { get; set; }
        public DriverLicenseType? DriverLicense { get; set; }
        public string MaritalStatus { get; set; }
        public string Education { get; set; }
        public List<string> Skills { get; set; } = new();
        public List<string> Languages { get; set; } = new();
        public List<string> Certificates { get; set; } = new();
    }
}
