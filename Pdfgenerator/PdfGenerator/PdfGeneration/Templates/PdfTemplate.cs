using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfGenerator;

public abstract class PdfTemplate : IPdfTemplate
{
    public abstract void Generate(Document document, PdfWriter writer, string jsonData);
    protected abstract void AddLogo(Document doc);

    protected void AddCellToHeader(PdfPTable table, string text, BaseColor bgColor)
    {
        Font font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);
        PdfPCell cell = new PdfPCell(new Phrase(text, font))
        {
            BackgroundColor = bgColor,
            HorizontalAlignment = Element.ALIGN_CENTER,
            Padding = 5
        };
        table.AddCell(cell);
    }

    protected void AddCellToBody(PdfPTable table, string text)
    {
        Font font = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
        PdfPCell cell = new PdfPCell(new Phrase(text, font))
        {
            HorizontalAlignment = Element.ALIGN_CENTER,
            Padding = 5
        };

        // Apply rounded corners
        cell.CellEvent = new RoundedCell(
            strokeColor: new BaseColor(18, 26, 42),
            fillColor: new BaseColor(252, 239, 229),
            radius: 0f
        );

        table.AddCell(cell);
    }

    protected void AddCoverPage(Document doc, PdfWriter writer, string titleText, string subTitleText)
    {
        PdfContentByte canvas = writer.DirectContentUnder;
        canvas.Rectangle(0, 0, doc.PageSize.Width, doc.PageSize.Height);
        canvas.SetColorFill(new BaseColor(252, 239, 229)); // Beige
        canvas.Fill();

        AddLogo(doc);

        Font coverFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, new BaseColor(18, 26, 42));
        Paragraph coverTitle = new Paragraph(titleText, coverFont);
        coverTitle.Alignment = Element.ALIGN_CENTER;
        coverTitle.SpacingBefore = 300;
        doc.Add(coverTitle);

        Paragraph subTitle = new Paragraph(subTitleText, FontFactory.GetFont(FontFactory.HELVETICA, 14));
        subTitle.Alignment = Element.ALIGN_CENTER;
        subTitle.SpacingBefore = 20;
        doc.Add(subTitle);
    }


}
