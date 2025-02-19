using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Service.DTOs.Candidates;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly AppDbContext _dbContext;

        public CandidateService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CandidateDto> CreateCandidateAsync(CandidateCreateDto dto)
        {
            var candidate = new Candidate
            {
                FullName = dto.FullName,
                Description = dto.Description,
                Birthday = dto.Birthday,
                BirthPlace = dto.BirthPlace,
                Gender = dto.Gender,
                MaritalStatus = dto.MaritalStatus,
                DriverLicense = dto.DriverLicense,
                Education = dto.Education,
                Skills = dto.Skills,
                Languages = dto.Languages,
                Certificates = dto.Certificates,
                PhoneNumbers = dto.PhoneNumbers.Select(n => new PhoneNumber { Number = n }).ToList(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _dbContext.Candidates.AddAsync(candidate);
            await _dbContext.SaveChangesAsync();

            return new CandidateDto
            {
                Id = candidate.Id,
                FullName = candidate.FullName,
                Description = candidate.Description,
                Birthday = candidate.Birthday,
                BirthPlace = candidate.BirthPlace,
                Gender = candidate.Gender.ToString(),
                MaritalStatus = candidate.MaritalStatus,
                DriverLicense = candidate.DriverLicense.ToString(),
                Education = candidate.Education,
                Skills = candidate.Skills,
                Languages = candidate.Languages,
                Certificates = candidate.Certificates,
                PhoneNumbers = candidate.PhoneNumbers.Select(p => p.Number).ToList()
            };
        }

        public async Task<CandidateDto> UpdateCandidateAsync(int id, CandidateEditDto dto)
        {
            var candidate = await _dbContext.Candidates
                .Include(c => c.PhoneNumbers)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (candidate == null)
            throw new KeyNotFoundException("Namizəd tapılmadı");

            if (!string.IsNullOrWhiteSpace(dto.FullName)) candidate.FullName = dto.FullName;
            if (!string.IsNullOrWhiteSpace(dto.Description)) candidate.Description = dto.Description;
            if (dto.Birthday.HasValue) candidate.Birthday = dto.Birthday.Value;
            if (!string.IsNullOrWhiteSpace(dto.BirthPlace)) candidate.BirthPlace = dto.BirthPlace;
            if (dto.Gender.HasValue) candidate.Gender = dto.Gender.Value;
            if (!string.IsNullOrWhiteSpace(dto.MaritalStatus)) candidate.MaritalStatus = dto.MaritalStatus;
            if (dto.DriverLicense.HasValue) candidate.DriverLicense = dto.DriverLicense.Value;
            if (!string.IsNullOrWhiteSpace(dto.Education)) candidate.Education = dto.Education;
            if (dto.Skills != null) candidate.Skills = dto.Skills;
            if (dto.Languages != null) candidate.Languages = dto.Languages;
            if (dto.Certificates != null) candidate.Certificates = dto.Certificates;

            if (dto.PhoneNumbers != null)
            {
                _dbContext.PhoneNumbers.RemoveRange(candidate.PhoneNumbers);
                candidate.PhoneNumbers = dto.PhoneNumbers.Select(n => new PhoneNumber { Number = n }).ToList();
            }

            candidate.UpdatedDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return new CandidateDto
            {
                Id = candidate.Id,
                FullName = candidate.FullName,
                Description = candidate.Description,
                Birthday = candidate.Birthday,
                BirthPlace = candidate.BirthPlace,
                Gender = candidate.Gender.ToString(),
                MaritalStatus = candidate.MaritalStatus,
                DriverLicense = candidate.DriverLicense.ToString(),
                Education = candidate.Education,
                Skills = candidate.Skills,
                Languages = candidate.Languages,
                Certificates = candidate.Certificates,
                PhoneNumbers = candidate.PhoneNumbers.Select(p => p.Number).ToList()
            };
        }

        public async Task<List<CandidateDto>> GetAllCandidatesAsync(int pageNumber, int pageSize)
        {
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var candidates = await _dbContext.Candidates
                .AsNoTrackingWithIdentityResolution()
                .Include(c => c.PhoneNumbers)
                .Include(c => c.Photos)
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return candidates.Select(c => new CandidateDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Description = c.Description,
                Birthday = c.Birthday,
                BirthPlace = c.BirthPlace,
                Gender = c.Gender.ToString(),
                MaritalStatus = c.MaritalStatus,
                DriverLicense = c.DriverLicense.ToString(),
                Education = c.Education,
                Skills = c.Skills,
                Languages = c.Languages,
                Certificates = c.Certificates,
                PhoneNumbers = c.PhoneNumbers.Select(p => p.Number).ToList(),
                MainPhotoUrl = c.Photos.FirstOrDefault(p => p.IsMain)?.Url
            }).ToList();
        }

        public async Task<CandidateDto> GetCandidateByIdAsync(int id)
        {
            var candidate = await _dbContext.Candidates
                .AsNoTrackingWithIdentityResolution()
                .Include(c => c.PhoneNumbers)
                .Include(c => c.Photos)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (candidate == null) throw new KeyNotFoundException("Namizəd Tapılmadı");

            return new CandidateDto
            {
                Id = candidate.Id,
                FullName = candidate.FullName,
                Description = candidate.Description,
                Birthday = candidate.Birthday,
                BirthPlace = candidate.BirthPlace,
                Gender = candidate.Gender.ToString(),
                MaritalStatus = candidate.MaritalStatus,
                DriverLicense = candidate.DriverLicense.ToString(),
                Education = candidate.Education,
                Skills = candidate.Skills,
                Languages = candidate.Languages,
                Certificates = candidate.Certificates,
                PhoneNumbers = candidate.PhoneNumbers.Select(p => p.Number).ToList(),
                MainPhotoUrl = candidate.Photos.FirstOrDefault(p => p.IsMain)?.Url
            };
        }

        public async Task<bool> DeleteCandidateAsync(int id)
        {
            var candidate = await _dbContext.Candidates
                .Include(c => c.Photos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (candidate == null) return false;
            if (candidate.Photos != null && candidate.Photos.Any())
            {
                foreach (var photo in candidate.Photos)
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), photo.Url.TrimStart('/'));

                    try
                    {
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                            Console.WriteLine($"Foto silindi: {filePath}");
                        }
                        else
                        {
                            Console.WriteLine($"Fayl tapılmadı: {filePath}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error fayl silinmədi {filePath}: {ex.Message}");
                    }
                }
            }
            _dbContext.Candidates.Remove(candidate);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAllCandidatesAsync()
        {
            var candidates = await _dbContext.Candidates
                .Include(c => c.Photos)
                .ToListAsync();

            if (!candidates.Any()) return false;
            foreach (var candidate in candidates)
            {
                if (candidate.Photos != null && candidate.Photos.Any())
                {
                    foreach (var photo in candidate.Photos)
                    {
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), photo.Url.TrimStart('/'));

                        try
                        {
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                                Console.WriteLine($"Foto silindi: {filePath}");
                            }
                            else
                            {
                                Console.WriteLine($"Fayl tapılmadı: {filePath}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error fayl silinmədi {filePath}: {ex.Message}");
                        }
                    }
                }
            }
            int affectedRows = await _dbContext.Candidates.ExecuteDeleteAsync();
            return affectedRows > 0;
        }

        public async Task<bool> SoftDeleteCandidateAsync(int id)
        {
            var candidate = await _dbContext.Candidates
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (candidate == null) return false;

            candidate.IsDeleted = true;
            candidate.DeletedDate = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
