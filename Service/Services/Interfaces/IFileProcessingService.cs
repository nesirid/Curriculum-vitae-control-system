using Microsoft.AspNetCore.Http;
using Service.DTOs.Candidates;
using Service.DTOs.FileControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IFileProcessingService
    {
        Task<FileProcessingResultDto> ProcessFileAsync(byte[] fileContent, string fileName);
        Dictionary<string, FileProcessingResultDto> GetAllCachedData();
    }
}
