using System.Collections.Generic;

namespace PdfGenerator;

public interface IPdfGeneratorService
{
    void CreatePdf(string json, string filePath, IPdfTemplate template);
}
