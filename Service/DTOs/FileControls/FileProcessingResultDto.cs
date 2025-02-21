﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.FileControls
{
    public class FileProcessingResultDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public DateTime? Birthday { get; set; }
        public string BirthPlace { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string DriverLicense { get; set; }
        public string Education { get; set; }
        public List<string> Skills { get; set; } = new();
        public List<string> Languages { get; set; } = new();
        public List<string> Certificates { get; set; } = new();
        public List<string> PhoneNumbers { get; set; } = new();
        public string WorkExperience { get; set; }

        public string CacheKey { get; set; }
    }
}
