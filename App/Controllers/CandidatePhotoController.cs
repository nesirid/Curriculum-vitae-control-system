using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Candidates;
using Service.Services.Interfaces;

namespace App.Controllers
{
    public class CandidatePhotoController : BaseController
    {
        private readonly ICandidatePhotoService _photoService;

        public CandidatePhotoController(ICandidatePhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost("{candidateId}/upload-photos")]
        public async Task<IActionResult> UploadPhotos(int candidateId, [FromForm] List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            return BadRequest("Fayllar mövcud deyil.");

            var uploadedPhotos = await _photoService.UploadPhotosAsync(candidateId, files);
            return Ok(uploadedPhotos);
        }

        [HttpPost("{candidateId}/set-main-photo/{photoId}")]
        public async Task<IActionResult> SetMainPhoto(int candidateId, int photoId)
        {
            var result = await _photoService.SetMainPhotoAsync(candidateId, photoId);
            return result ? Ok("Əsas şəkil yeniləndi")
                          : BadRequest("Əsas fotonu yeniləyərkən xəta baş verdi");
        }

        [HttpDelete("{candidateId}/delete-photo/{photoId}")]
        public async Task<IActionResult> DeletePhoto(int candidateId, int photoId)
        {
            var result = await _photoService.DeletePhotoAsync(candidateId, photoId);
            if (!result) return NotFound("Əsas şəkil silinə bilməz və ya Şəkil tapılmadı");
            return result ? Ok("Foto silindi") : NotFound("Foto tapılmadı");
        }

        [HttpGet("{candidateId}/photos")]
        public async Task<IActionResult> GetPhotos(int candidateId)
        {
            var photos = await _photoService.GetPhotosByCandidateIdAsync(candidateId);
            if (photos == null || photos.Count == 0)
            return NotFound("Foto tapılmadı nanizəd mövcud deyil");
            return Ok(photos);
        }
    }
}
