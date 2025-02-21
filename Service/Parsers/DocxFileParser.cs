using Service.Parsers.Interfaces;
using System.Text;
using DocumentFormat.OpenXml.Packaging;

namespace Service.Parsers
{
    public class DocxFileParser : IFileParser
    {
        public async Task<string> ExtractTextAsync(byte[] fileContent)
        {
            using var stream = new MemoryStream(fileContent);
            using var wordDoc = WordprocessingDocument.Open(stream, false);

            StringBuilder text = new StringBuilder();
            foreach (var textElement in wordDoc.MainDocumentPart.Document.Body.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>())
            {
                text.AppendLine(textElement.Text);
            }

            return await Task.FromResult(text.ToString());
        }
    }
}
