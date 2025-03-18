using System.Collections.Generic;

namespace PdfGenerator;

public interface IPdfGeneratorService
{
    void CreatePdf(List<Person> people, string filePath, string logoPath);
}
