
namespace Service.Services.Interfaces
{
    public interface IPhotoFileTypeRecognizer
    {
        string RecognizeFileType(byte[] fileContent, string fileName);
    }
}
