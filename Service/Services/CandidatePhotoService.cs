using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Data;
using Service.DTOs.Candidates;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class CandidatePhotoService : ICandidatePhotoService
    {
        private readonly AppDbContext _dbContext;
        private readonly string _uploadsFolder;

        public CandidatePhotoService(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _uploadsFolder = configuration["FileStorage:UploadPath"] ?? "wwwroot/uploads";

            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
        }

        public async Task<List<CandidatePhotoDto>> UploadPhotosAsync(int candidateId, List<IFormFile> files)
        {
            var candidate = await _dbContext.Candidates
                .Include(c => c.Photos)
                .FirstOrDefaultAsync(c => c.Id == candidateId);

            if (candidate == null) throw new KeyNotFoundException("Namizəd tapılmadı");

            if (files == null || files.Count == 0) throw new ArgumentException("Fayllar mövcud deyil");

            var uploadedPhotos = new List<CandidatePhotoDto>();
            const long maxFileSize = 5 * 1024 * 1024;

            foreach (var file in files)
            {
                if (file.Length > maxFileSize)
                    throw new ArgumentException($"Fayl '{file.FileName}' çox böyükdür. Maksimum icazə verilən ölçü 5MB-dir.");

                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(_uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var newPhoto = new CandidatePhoto
                {
                    CandidateId = candidateId,
                    Url = $"uploads/{fileName}".Replace("\\", "/"),
                    IsMain = candidate.Photos.Count == 0
                };
                await _dbContext.CandidatePhotos.AddAsync(newPhoto);
                uploadedPhotos.Add(new CandidatePhotoDto
                {
                    Id = newPhoto.Id,
                    Url = newPhoto.Url,
                    IsMain = newPhoto.IsMain
                });
            }
            await _dbContext.SaveChangesAsync();
            return uploadedPhotos;
        }

        public async Task<bool> SetMainPhotoAsync(int candidateId, int photoId)
        {
            var candidate = await _dbContext.Candidates
                .Include(c => c.Photos)
                .FirstOrDefaultAsync(c => c.Id == candidateId);

            if (candidate == null) return false;

            var photo = candidate.Photos.FirstOrDefault(p => p.Id == photoId);
            if (photo == null) return false;

            foreach (var p in candidate.Photos) p.IsMain = false;
            photo.IsMain = true;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePhotoAsync(int candidateId, int photoId)
        {
            var photo = await _dbContext.CandidatePhotos
                .FirstOrDefaultAsync(p => p.Id == photoId && p.CandidateId == candidateId);

            if (photo == null) return false;

            if (photo.IsMain)  return false;

            var filePath = Path.Combine(_uploadsFolder, photo.Url.TrimStart('/'));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            _dbContext.CandidatePhotos.Remove(photo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<CandidatePhotoDto>> GetPhotosByCandidateIdAsync(int candidateId)
        {
            bool candidateExists = await _dbContext.Candidates
                .AsNoTracking()
                .AnyAsync(c => c.Id == candidateId);

            if (!candidateExists)
                return new List<CandidatePhotoDto>();

            return await _dbContext.CandidatePhotos
                .AsNoTrackingWithIdentityResolution()
                .Where(p => p.CandidateId == candidateId)
                .Select(p => new CandidatePhotoDto
                {
                    Id = p.Id,
                    Url = p.Url,
                    IsMain = p.IsMain
                })
                .ToListAsync();
        }
    }
}
