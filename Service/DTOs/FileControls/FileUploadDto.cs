﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.FileControls
{
    public class FileUploadDto
    {
        public IFormFile File { get; set; }
    }
}
