using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfGenerator;

public class GradientBackground
{
    private BaseColor startColor;
    private BaseColor endColor;
    private GradientDirection gradientDirection;
    private float powerX;
    private float powerY;

    public GradientBackground(BaseColor startColor, BaseColor endColor, GradientDirection gradientDirection, float powerX = 1f, float powerY = 1f)
    {
        this.startColor = startColor;
        this.endColor = endColor;
        this.gradientDirection = gradientDirection;
        this.powerX = powerX;
        this.powerY = powerY;
    }

    public void ApplyGradient(PdfContentByte canvas, Rectangle pageSize)
    {
        PdfShading shading = null;


        float x1 = pageSize.Left, y1 = pageSize.Bottom;
        float x2 = pageSize.Right, y2 = pageSize.Top;

        switch (gradientDirection)
        {
            case GradientDirection.Top:
                x1 = x2 = pageSize.Width / 2;
                y1 = pageSize.Bottom;
                y2 = pageSize.Top;
                break;
            case GradientDirection.Bottom:
                x1 = x2 = pageSize.Width / 2;
                y1 = pageSize.Top;
                y2 = pageSize.Bottom;
                break;
            case GradientDirection.Left:
                y1 = y2 = pageSize.Height / 2;
                x1 = pageSize.Right;
                x2 = pageSize.Left;
                break;
            case GradientDirection.Right:
                y1 = y2 = pageSize.Height / 2;
                x1 = pageSize.Left;
                x2 = pageSize.Right;
                break;
            case GradientDirection.TopLeft:
                x1 = pageSize.Right * (1 - powerX);
                y1 = pageSize.Bottom;
                x2 = pageSize.Left;
                y2 = pageSize.Top * powerY;
                break;
            case GradientDirection.TopRight:
                x1 = pageSize.Left * powerX;
                y1 = pageSize.Bottom;
                x2 = pageSize.Right;
                y2 = pageSize.Top * powerY;
                break;
            case GradientDirection.BottomLeft:
                x1 = pageSize.Right * (1 - powerX);
                y1 = pageSize.Top;
                x2 = pageSize.Left;
                y2 = pageSize.Bottom * powerY;
                break;
            case GradientDirection.BottomRight:
                x1 = pageSize.Left * powerX;
                y1 = pageSize.Top;
                x2 = pageSize.Right;
                y2 = pageSize.Bottom * powerY;
                break;
        }

        shading = PdfShading.SimpleAxial(
            canvas.PdfWriter, x1, y1, x2, y2,
            new BaseColor(startColor.R, startColor.G, startColor.B),
            new BaseColor(endColor.R, endColor.G, endColor.B),
            true, true
        );

        PdfShadingPattern pattern = new PdfShadingPattern(shading);
        canvas.SetShadingFill(pattern);
        canvas.Rectangle(pageSize.Left, pageSize.Bottom, pageSize.Width, pageSize.Height);
        canvas.Fill();
    }
}
