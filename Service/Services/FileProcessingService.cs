using Microsoft.Extensions.Caching.Memory;
using Service.Parsers.Interfaces;
using Service.Parsers;
using Service.Services.Interfaces;
using System.Text.RegularExpressions;
using Service.DTOs.FileControls;
using System.Reflection;
using Service.Services.Helpers;

namespace Service.Services
{
    public class FileProcessingService : IFileProcessingService
    {
        private readonly IDocFileTypeRecognizer _fileTypeRecognizer;
        private readonly Dictionary<string, IFileParser> _parsers;
        private readonly IMemoryCache _cache;
        private const string CacheKeyPrefix = "FileProcessing_";

        public FileProcessingService(
            IDocFileTypeRecognizer fileTypeRecognizer,
            IEnumerable<IFileParser> parsers,
            IMemoryCache cache)
        {
            _fileTypeRecognizer = fileTypeRecognizer;
            _cache = cache;

            _parsers = new Dictionary<string, IFileParser>();

            foreach (var parser in parsers)
            {
                if (parser is PdfFileParser) _parsers["pdf"] = parser;
                if (parser is DocxFileParser) _parsers["docx"] = parser;
                //if (parser is DocFileParser) _parsers["doc"] = parser;
            }
        }
        public async Task<FileProcessingResultDto> ProcessFileAsync(byte[] fileContent, string fileName)
        {
            const long fileContentSize = 5 * 1024 * 1024;

            if (fileContent.Length > fileContentSize)
                throw new ArgumentException($"Faylın ulcusu maksimum 5mb ola bilər");

            string fileType = _fileTypeRecognizer.RecognizeFileType(fileContent, fileName);

            if (!_parsers.ContainsKey(fileType))
                throw new NotSupportedException($"Fayl tipi {fileType} dəstəklənmir.");

            string text = await _parsers[fileType].ExtractTextAsync(fileContent);

            var extractedData = ExtractDataFromText(text);

            string cacheKey = Guid.NewGuid().ToString();
            _cache.Set(cacheKey, extractedData, TimeSpan.FromMinutes(1));

            extractedData.CacheKey = cacheKey;
            return extractedData;
        }

        private FileProcessingResultDto ExtractDataFromText(string text)
        {
            var extractedData = new FileProcessingResultDto();

            foreach (var pattern in RegexPatterns.Patterns)
            {
                var match = Regex.Match(text, pattern.Value, RegexOptions.Multiline);
                if (match.Success)
                {
                    string cleanValue = match.Groups[1].Value.Trim()
                        .Replace("\n", " ") 
                        .Replace("\r", " ")
                        .Replace("\t", " ")
                        .Replace(",", ";");   

                    switch (pattern.Key)
                    {
                        case "FullName":
                            extractedData.FullName = cleanValue;
                            break;
                        case "Description":
                            extractedData.Description = cleanValue;
                            break;
                        case "Email":
                            extractedData.Email = match.Groups[0].Value.Trim();
                            break;
                        case "PhoneNumbers":
                            extractedData.PhoneNumbers.Add(cleanValue);
                            break;
                        case "Birthday":
                            extractedData.Birthday = DateTime.TryParse(cleanValue, out var date) ? date : null;
                            break;
                        case "BirthPlace":
                            extractedData.BirthPlace = cleanValue;
                            break;
                        case "Gender":
                            extractedData.Gender = cleanValue;
                            break;
                        case "MaritalStatus":
                            extractedData.MaritalStatus = cleanValue;
                            break;
                        case "DriverLicense":
                            extractedData.DriverLicense = cleanValue;
                            break;
                        case "Education":
                            extractedData.Education = cleanValue;
                            break;
                        case "Skills":
                            extractedData.Skills.Add(cleanValue);
                            break;
                        case "Languages":
                            extractedData.Languages.Add(cleanValue);
                            break;
                        case "WorkExperience":
                            extractedData.WorkExperience = cleanValue;
                            break;
                        case "Certificates":
                            extractedData.Certificates.Add(cleanValue);
                            break;
                    }
                }
            }

            return extractedData;
        }

        public Dictionary<string, FileProcessingResultDto> GetAllCachedData()
        {
            var cachedData = new Dictionary<string, FileProcessingResultDto>();

            var cacheEntriesCollection = typeof(MemoryCache)
                .GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.GetValue(_cache) as dynamic;

            if (cacheEntriesCollection != null)
            {
                foreach (var cacheItem in cacheEntriesCollection)
                {
                    var key = cacheItem.Key as string;
                    if (!string.IsNullOrEmpty(key) && key.StartsWith(CacheKeyPrefix))
                    {
                        if (_cache.TryGetValue(key, out FileProcessingResultDto fileData))
                        {
                            cachedData[key] = fileData;
                        }
                    }
                }
            }

            return cachedData;
        }

    }
}
