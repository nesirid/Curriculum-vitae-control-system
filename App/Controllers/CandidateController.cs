using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Candidates;
using Service.Services.Interfaces;

namespace App.Controllers
{
    public class CandidateController : BaseController
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCandidate([FromForm] CandidateCreateDto dto)
        {
            if (dto == null)
            return BadRequest("Etibarsız namizəd məlumatları");

            var createdCandidate = await _candidateService.CreateCandidateAsync(dto);
            return CreatedAtAction(nameof(GetCandidateById), new { id = createdCandidate.Id }, createdCandidate);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCandidate(int id, [FromForm] CandidateEditDto dto)
        {
            if (dto == null)
            return BadRequest("Etibarsız namizəd məlumatları");

            var updatedCandidate = await _candidateService.UpdateCandidateAsync(id, dto);
            return Ok(updatedCandidate);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCandidates([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var candidates = await _candidateService.GetAllCandidatesAsync(pageNumber, pageSize);
            return Ok(candidates);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidateById(int id)
        {
            var candidate = await _candidateService.GetCandidateByIdAsync(id);
            return Ok(candidate);
        }

        [HttpDelete("soft-delete/{id}")]
        public async Task<IActionResult> SoftDeleteCandidate(int id)
        {
            bool result = await _candidateService.SoftDeleteCandidateAsync(id);
            if (!result) return NotFound("Namizəd tapılmadı");
            return Ok("Namizəd uğurla silindi");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            bool result = await _candidateService.DeleteCandidateAsync(id);
            if (!result) return NotFound("Namizəd tapılmadı");
            return Ok("Namizəd uğurla silindi");
        }

        [HttpDelete("delete-all")]
        public async Task<IActionResult> DeleteAllCandidates()
        {
            bool result = await _candidateService.DeleteAllCandidatesAsync();
            if (!result) return NotFound("Silmək üçün namizəd tapılmadı");
            return Ok("Bütün namizədlər uğurla silindi");
        }
    }
}
