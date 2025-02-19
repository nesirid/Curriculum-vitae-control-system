using Domain.Entities;
using Service.DTOs.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICandidateService
    {
        Task<CandidateDto> CreateCandidateAsync(CandidateCreateDto dto);
        Task<CandidateDto> UpdateCandidateAsync(int id, CandidateEditDto dto);
        Task<List<CandidateDto>> GetAllCandidatesAsync(int pageNumber, int pageSize);
        Task<CandidateDto> GetCandidateByIdAsync(int id);
        Task<bool> DeleteCandidateAsync(int id);
        Task<bool> DeleteAllCandidatesAsync();
        Task<bool> SoftDeleteCandidateAsync(int id);
    }
}
