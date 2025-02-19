using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.DTOs.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICandidatePhotoService
    {
        Task<List<CandidatePhotoDto>> UploadPhotosAsync(int candidateId, List<IFormFile> files);
        Task<bool> SetMainPhotoAsync(int candidateId, int photoId);
        Task<bool> DeletePhotoAsync(int candidateId, int photoId);
        Task<List<CandidatePhotoDto>> GetPhotosByCandidateIdAsync(int candidateId);
    }
}
