using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;

namespace PdfGenerator
{
    public class TestPdfTemplate : PdfTemplate
    {
        private const string LogoPath = "C:\\Users\\nielsD\\source\\repos\\PdfGenerator\\PdfGenerator\\logo.png";

        public override void Generate(Document document, PdfWriter writer, string jsonData)
        {
            var people = JsonConvert.DeserializeObject<List<Person>>(jsonData);


            writer.PageEvent = new PdfFooter();

            document.AddAuthor("Qii Report");
            document.SetMargins(40, 40, 40, 60);

            document.Open();

            PdfContentByte canvas = writer.DirectContentUnder;

            canvas.SetColorFill(new BaseColor(252, 239, 229));
            canvas.Rectangle(0, 0, PageSize.A4.Width, PageSize.A4.Height);
            canvas.Fill();

            AddCoverPage(document, writer, "MyQii Report", "Bank & User Data");

            document.NewPage();

            GradientBackground gradient = new GradientBackground(
                new BaseColor(18, 26, 42),
                new BaseColor(252, 239, 229),
                GradientDirection.Bottom,
                powerX: 0.1f,
                powerY: 0.2f
            );

            gradient.ApplyGradient(writer.DirectContentUnder, document.PageSize);

            AddLogo(document);

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

        protected override void AddLogo(Document doc)
        {
            if (File.Exists(LogoPath))
            {
                Image logo = Image.GetInstance(LogoPath);
                logo.ScaleAbsolute(120f, 120f);
                logo.SetAbsolutePosition(
                    (doc.PageSize.Width - logo.ScaledWidth) / 2,
                    doc.PageSize.Height - 200
                );
                doc.Add(logo);
            }
        }
    }
}
