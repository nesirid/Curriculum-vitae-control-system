using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Service.DTOs.FileControls;
using Service.Services.Interfaces;


namespace App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileProcessingController : BaseController
    {
        private readonly IFileProcessingService _fileProcessingService;
        private readonly IMemoryCache _cache;

        public FileProcessingController(IFileProcessingService fileProcessingService, IMemoryCache cache)
        {
            _fileProcessingService = fileProcessingService;
            _cache = cache;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("Fayl əlavə olunmadı");

            using var stream = new MemoryStream();
            await dto.File.CopyToAsync(stream);
            byte[] fileContent = stream.ToArray();

            var extractedData = await _fileProcessingService.ProcessFileAsync(fileContent, dto.File.FileName);

            if (extractedData == null)
                return BadRequest("Fayl formatı dəstəklənmir və ya məlumat tapılmadı");

            return Ok(extractedData);
        }

        [HttpGet("{cacheKey}")]
        public IActionResult GetCachedData(string cacheKey)
        {
            if (_cache.TryGetValue<FileProcessingResultDto>(cacheKey, out var data))
            {
                return Ok(data);
            }

            return NotFound();
        }

        [HttpDelete("{cacheKey}")]
        public IActionResult RemoveCachedData(string cacheKey)
        {
            if (!_cache.TryGetValue(cacheKey, out _))
            {
                return NotFound(new { message = $"Cache-de '{cacheKey}' məlumat tapılmadı" });
            }

            _cache.Remove(cacheKey);
            return Ok(new { message = $"Cache '{cacheKey}' uğurla silindi" });
        }
    }
}
