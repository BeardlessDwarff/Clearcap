using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PdfGenerator;

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
                { 'Name': 'vladimir', 'Age': 32, 'Email': 'vladdyD@bats.com', 'BankAccount': { 'AccountNumber': '112233445', 'BankName': 'TheLiberMortis', 'Balance': 3400.80 } },
                { 'Name': 'Elspeth von Draken', 'Age': 27, 'Email': 'shadowOfBlack@humane.com', 'BankAccount': { 'AccountNumber': '334455667', 'BankName': 'OnceMoreToTheBreach', 'Balance': 15000.30 } },
                { 'Name': 'Horus', 'Age': 29, 'Email': 'H.H@human.com', 'BankAccount': { 'AccountNumber': '998877665', 'BankName': 'Heresy', 'Balance': 7600.60 } }
            ]";

        var people = JsonConvert.DeserializeObject<List<Person>>(jsonData);

        IPdfGeneratorService pdfGeneratorService = new PdfGeneratorService();
        IPdfTemplate template = new TestPdfTemplate();
        pdfGeneratorService.CreatePdf(people, "output.pdf", template);

        Console.WriteLine("PDF created successfully!");
    }
}
