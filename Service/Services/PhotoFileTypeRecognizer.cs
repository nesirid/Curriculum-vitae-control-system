using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PhotoFileTypeRecognizer : IPhotoFileTypeRecognizer
    {
        private static readonly string[]
        PhotoExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

        public string RecognizeFileType(byte[] fileContent, string fileName)
        {
            var extension = Path.GetExtension(fileName)?.ToLower().Trim();
            return PhotoExtensions.Contains(extension) ? "photo" : "unknown";
        }
    }
}
