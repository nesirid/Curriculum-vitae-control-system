﻿using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class DocFileTypeRecognizer : IDocFileTypeRecognizer
    {
        public string RecognizeFileType(byte[] fileContent, string fileName)
        {
            var extension = Path.GetExtension(fileName)?.ToLower().Trim();

            return extension switch
            {
                ".doc" => "doc",
                ".docx" => "docx",
                ".pdf" => "pdf",
                _ => "unknown"
            };
        }
    }
}
