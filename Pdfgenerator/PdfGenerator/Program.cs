using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using PdfGenerator;


struct uitgave
{
    public string naam;
    public double bedrag;
    public string datum;
}

class Program
{
    static void Main()
    {
        string jsonData = @"
        [
            { 'Name': 'GromThePaunch', 'Age': 30, 'Email': 'gromTheBest@greens.com', 'BankAccount': { 'AccountNumber': '123456789', 'BankName': 'TheBiggestBank', 'Balance': 5000.75 } },
            { 'Name': 'GrimgorIronHide', 'Age': 25, 'Email': 'bestSmile@greens.com', 'BankAccount': { 'AccountNumber': '987654321', 'BankName': 'Waaaaagh', 'Balance': 12000.50 } },
            { 'Name': 'queekHeadtaker', 'Age': 35, 'Email': 'bestStick@tail.com', 'BankAccount': { 'AccountNumber': '555333111', 'BankName': 'YesYes', 'Balance': 2200.00 } },
            { 'Name': 'throtTheunclean', 'Age': 28, 'Email': 'wicket.claw@tail.com', 'BankAccount': { 'AccountNumber': '777888999', 'BankName': 'MOREWARPSTONE', 'Balance': 8500.00 } },
            { 'Name': 'ikitClaw', 'Age': 40, 'Email': 'wicket.claw@tail.com', 'BankAccount': { 'AccountNumber': '654321987', 'BankName': 'BESTINVENTOR', 'Balance': 45000.25 } },
            { 'Name': 'vladtheimpaler', 'Age': 32, 'Email': 'vladdyD@bats.com', 'BankAccount': { 'AccountNumber': '112233445', 'BankName': 'TheLiberMortis', 'Balance': 3400.80 } },
            { 'Name': 'Elspeth von Draken', 'Age': 27, 'Email': 'shadowOfBlack@humane.com', 'BankAccount': { 'AccountNumber': '334455667', 'BankName': 'OnceMoreToTheBreach', 'Balance': 15000.30 } },
            { 'Name': 'Horus', 'Age': 29, 'Email': 'H.H@human.com', 'BankAccount': { 'AccountNumber': '998877665', 'BankName': 'Heresy', 'Balance': 7600.60 } }
        ]";

        var people = JsonConvert.DeserializeObject<List<Person>>(jsonData);

        CreatePdf(people, "output.pdf", "C:\\Users\\nielsD\\source\\repos\\PdfGenerator\\PdfGenerator\\logo.png");

        Console.WriteLine("PDF created successfully!");
    }

    public static void CreatePdf(List<Person> people, string filePath, string logoPath)
    {
        Document document = new Document(PageSize.A4);
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

        writer.PageEvent = new PdfFooter();

        document.AddAuthor("Qii Report");
        document.SetMargins(40, 40, 40, 60);

        document.Open();

        PdfContentByte canvas = writer.DirectContentUnder;

        canvas.SetColorFill(new BaseColor(252, 239, 229)); 
        canvas.Rectangle(0, 0, PageSize.A4.Width, PageSize.A4.Height);
        canvas.Fill();




        AddCoverPage(document, writer, logoPath);

        document.NewPage();

        GradientBackground gradient = new GradientBackground(
            new BaseColor(18, 26, 42),
            new BaseColor(252, 239, 229),
            GradientDirection.Bottom,
            powerX: 0.1f,
            powerY: 0.2f
        );

        gradient.ApplyGradient(writer.DirectContentUnder, document.PageSize);

        if (File.Exists(logoPath))
        {
            Image logo = Image.GetInstance(logoPath);
            logo.ScaleAbsolute(80f, 80f);
            logo.Alignment = Image.ALIGN_CENTER;
            document.Add(logo);
        }

        Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, new BaseColor(18, 26, 42)); // Dark Blue
        Paragraph title = new Paragraph("User & Bank Report", titleFont);
        title.Alignment = Element.ALIGN_CENTER;
        title.SpacingAfter = 20;
        document.Add(title);

        PdfPTable userTable = new PdfPTable(3);
        userTable.WidthPercentage = 100;
        userTable.SetWidths(new float[] { 2, 1, 3 });

        AddCellToHeader(userTable, "Name", new BaseColor(18, 26, 42));
        AddCellToHeader(userTable, "Age", new BaseColor(18, 26, 42));
        AddCellToHeader(userTable, "Email", new BaseColor(18, 26, 42));

        foreach (var person in people)
        {
            AddCellToBody(userTable, person.Name);
            AddCellToBody(userTable, person.Age.ToString());
            AddCellToBody(userTable, person.Email);
        }

        document.Add(userTable);

        Font sectionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, new BaseColor(224, 82, 77)); // Red
        Paragraph bankTitle = new Paragraph("\nBank Account Details", sectionFont);
        bankTitle.Alignment = Element.ALIGN_CENTER;
        bankTitle.SpacingAfter = 10;
        document.Add(bankTitle);

        PdfPTable bankTable = new PdfPTable(3);
        bankTable.WidthPercentage = 100;
        bankTable.SetWidths(new float[] { 3, 3, 2 });

        AddCellToHeader(bankTable, "Bank Name", new BaseColor(18, 26, 42));
        AddCellToHeader(bankTable, "Account Number", new BaseColor(18, 26, 42));
        AddCellToHeader(bankTable, "Balance ($)", new BaseColor(18, 26, 42));

        foreach (var person in people)
        {
            AddCellToBody(bankTable, person.BankAccount.BankName);
            AddCellToBody(bankTable, person.BankAccount.AccountNumber);
            AddCellToBody(bankTable, person.BankAccount.Balance.ToString("C"));
        }

        document.Add(bankTable);

        document.Close();
    }

    private static void AddCellToHeader(PdfPTable table, string text, BaseColor bgColor)
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

    private static void AddCellToBody(PdfPTable table, string text)
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

    private static void AddCoverPage(Document doc, PdfWriter writer, string logoPath)
    {
        PdfContentByte canvas = writer.DirectContentUnder;
        canvas.Rectangle(0, 0, doc.PageSize.Width, doc.PageSize.Height);
        canvas.SetColorFill(new BaseColor(252, 239, 229)); // Beige
        canvas.Fill();

        if (File.Exists(logoPath))
        {
            Image logo = Image.GetInstance(logoPath);
            logo.ScaleAbsolute(120f, 120f);
            logo.SetAbsolutePosition(
                (doc.PageSize.Width - logo.ScaledWidth) / 2,
                doc.PageSize.Height - 200
            );
            doc.Add(logo);
        }

        Font coverFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, new BaseColor(18, 26, 42)); 
        Paragraph coverTitle = new Paragraph("MyQii Report", coverFont);
        coverTitle.Alignment = Element.ALIGN_CENTER;
        coverTitle.SpacingBefore = 300;
        doc.Add(coverTitle);

        Paragraph subTitle = new Paragraph("Bank & User Data", FontFactory.GetFont(FontFactory.HELVETICA, 14));
        subTitle.Alignment = Element.ALIGN_CENTER;
        subTitle.SpacingBefore = 20;
        doc.Add(subTitle);
    }
}

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

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public BankAccount BankAccount { get; set; }
}

public class BankAccount
{
    public string AccountNumber { get; set; }
    public string BankName { get; set; }
    public double Balance { get; set; }
}
