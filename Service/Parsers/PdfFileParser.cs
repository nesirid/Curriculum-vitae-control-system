using Service.Parsers.Interfaces;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace Service.Parsers
{
    public class PdfFileParser : IFileParser
    {
        public async Task<string> ExtractTextAsync(byte[] fileContent)
        {
            using var stream = new MemoryStream(fileContent);
            using var reader = new PdfReader(stream);
            using var pdfDoc = new PdfDocument(reader);

            StringBuilder text = new StringBuilder();
            for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
            {
                text.AppendLine(PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i)));
            }

            return await Task.FromResult(text.ToString());
        }
    }
}
