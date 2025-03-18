using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfGenerator;

public class PdfGeneratorService : IPdfGeneratorService
{
    public void CreatePdf(List<Person> people, string filePath, IPdfTemplate template)
    {
        Document document = new Document(PageSize.A4);
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

        template.Generate(document, writer, people);
    }
}
