using iTextSharp.text;
using iTextSharp.text.pdf;
using System;

namespace PdfGenerator;

public class PdfFooter : PdfPageEventHelper
{
    public override void OnEndPage(PdfWriter writer, Document document)
    {
        PdfPTable footerTable = new PdfPTable(2);
        footerTable.TotalWidth = document.PageSize.Width - 80;
        footerTable.SetWidths(new float[] { 6, 1 });

        Font footerFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, new BaseColor(18, 26, 42)); // Dark Blue
        PdfPCell dateCell = new PdfPCell(new Phrase($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm}", footerFont));
        dateCell.Border = PdfPCell.NO_BORDER;
        dateCell.HorizontalAlignment = Element.ALIGN_LEFT;

        PdfPCell pageCell = new PdfPCell(new Phrase($"Page {writer.PageNumber}", footerFont));
        pageCell.Border = PdfPCell.NO_BORDER;
        pageCell.HorizontalAlignment = Element.ALIGN_RIGHT;

        footerTable.AddCell(dateCell);
        footerTable.AddCell(pageCell);
        footerTable.WriteSelectedRows(0, -1, 40, 30, writer.DirectContent);
    }
}
