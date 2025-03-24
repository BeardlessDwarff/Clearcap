using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfGenerator.PdfGeneration.Templates.HafkampTemplateTest1;

public class HafkampTemplateTest1 : PdfTemplate
{
    public override void Generate(Document document, PdfWriter writer, string jsonData)
    {
        try
        {
            DataHafkamp data = JsonConvert.DeserializeObject<DataHafkamp>(jsonData);

            // Add the date the pdf was generated on the bottom left. And page number on the bottom right
            writer.PageEvent = new PdfFooter();

            document.AddAuthor("ClearCap Report");
            document.SetMargins(40, 40, 40, 60);

            document.Open();

            AddLogo(document);

            AddSectionHeader(document, "Persoonsgegevens", "A. van der Kamp", "Toetsing op: 02/03/2025");

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error generating PDF: " + ex.Message);
        }
        finally
        {
            document.Close();
        }
    }

    protected override void AddLogo(Document doc)
    {
        try
        {
            string imagePath = @"..\..\TestData\Pic\hafkamp1.png"; 
            Image logo = Image.GetInstance(imagePath);
            logo.ScaleToFit(300, 100); // TODO: fucks up the scaling but for testing is fine
            logo.Alignment = Element.ALIGN_LEFT;
            doc.Add(logo);

            doc.Add(new Paragraph("\n\n\n"));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding logo: " + ex.Message);
        }
    }

    private void AddSectionHeader(Document doc, string leftText, string rightText, string subtitle)
    {
        PdfPTable table = new PdfPTable(3)
        {
            WidthPercentage = 100
        };
        table.SetWidths(new float[] { 40, 20, 40 }); // Split the line into 3 column and determine width


        Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
        Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 14, BaseColor.BLACK);

        // Left-aligned Title
        PdfPCell leftCell = new PdfPCell(new Phrase(leftText, boldFont))
        {
            Border = PdfPCell.NO_BORDER,
            HorizontalAlignment = Element.ALIGN_LEFT
        };

        PdfPCell spacerCell = new PdfPCell(new Phrase(" "))
        {
            Border = PdfPCell.NO_BORDER
        };

        // Right-aligned Name
        PdfPCell rightCell = new PdfPCell(new Phrase(rightText, boldFont))
        {
            Border = PdfPCell.NO_BORDER,
            HorizontalAlignment = Element.ALIGN_RIGHT
        };

        table.AddCell(leftCell);
        table.AddCell(spacerCell);
        table.AddCell(rightCell);

        doc.Add(table);

        // Add a black line
        Paragraph blackLine = new Paragraph(" ")
        {
            SpacingBefore = 0,
            SpacingAfter = 2
        };
        blackLine.Add(new Chunk(new LineSeparator(7f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, 5)));
        doc.Add(blackLine);

        PdfPTable subtitleTable = new PdfPTable(1)
        {
            WidthPercentage = 100
        };

        PdfPCell subtitleCell = new PdfPCell(new Phrase(subtitle, normalFont))
        {
            Border = PdfPCell.NO_BORDER,
            HorizontalAlignment = Element.ALIGN_LEFT
        };

        subtitleTable.AddCell(subtitleCell);

        doc.Add(subtitleTable);
    }
}
