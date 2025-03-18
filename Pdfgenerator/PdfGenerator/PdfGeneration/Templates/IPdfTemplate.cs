using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfGenerator;

public interface IPdfTemplate
{
    void Generate(Document document, PdfWriter writer, List<Person> people);
}
