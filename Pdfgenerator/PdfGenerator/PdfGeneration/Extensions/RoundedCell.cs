using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfGenerator;

public class RoundedCell : IPdfPCellEvent
{
    private BaseColor strokeColor;
    private BaseColor fillColor;
    private float radius;

    public RoundedCell(BaseColor strokeColor, BaseColor fillColor, float radius)
    {
        this.strokeColor = strokeColor;
        this.fillColor = fillColor;
        this.radius = radius;
    }

    public void CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases)
    {
        PdfContentByte bgCanvas = canvases[PdfPTable.BACKGROUNDCANVAS];
        bgCanvas.SetColorFill(fillColor);
        bgCanvas.RoundRectangle(
            position.Left + 1.5f,
            position.Bottom + 1.5f,
            position.Width - 3,
            position.Height - 3,
            radius
        );
        bgCanvas.Fill();

        PdfContentByte borderCanvas = canvases[PdfPTable.LINECANVAS];
        borderCanvas.SetLineWidth(1f);
        borderCanvas.SetColorStroke(strokeColor);
        borderCanvas.RoundRectangle(
            position.Left + 1.5f,
            position.Bottom + 1.5f,
            position.Width - 3,
            position.Height - 3,
            radius
        );
        borderCanvas.Stroke();
    }
}
