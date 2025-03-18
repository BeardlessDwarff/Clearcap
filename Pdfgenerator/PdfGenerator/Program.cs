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
                { 'Name': 'batsy', 'Age': 32, 'Email': 'batsy@bats.com', 'BankAccount': { 'AccountNumber': '112233445', 'BankName': 'TheLiberMortis', 'Balance': 3400.80 } },
                { 'Name': 'Elspeth von Draken', 'Age': 27, 'Email': 'shadowOfBlack@humane.com', 'BankAccount': { 'AccountNumber': '334455667', 'BankName': 'OnceMoreToTheBreach', 'Balance': 15000.30 } },
                { 'Name': 'Horus', 'Age': 29, 'Email': 'H.H@human.com', 'BankAccount': { 'AccountNumber': '998877665', 'BankName': 'Heresy', 'Balance': 7600.60 } }
            ]";


        string jsonDataHafkamp = @"
{
  ""persoon"": {
    ""identiteit"": {
      ""voornaam"": ""Andre"",
      ""tussenvoegsel"": ""Van der"",
      ""achternaam"": ""Kamp"",
      ""geboortedatum"": ""1978-03-01"",
      ""nationaliteit"": ""Nederlands""
    },
    ""contactgegevens"": {
      ""email"": ""test@email.com"",
      ""telefoonnummer"": ""0612345678""
    },
    ""adres"": {
      ""straat"": ""Eusebiusstraat"",
      ""huisnummer"": ""1"",
      ""postcode"": ""6846XX"",
      ""plaats"": ""Arnhem""
    },
    ""huishouden"": {
      ""burgerlijke_staat"": ""Getrouwd"",
      ""geregistreerde_partner"": 1,
      ""inwonende_kinderen"": 2
    }
  },
  ""financien"": {
    ""besteedbaar_inkomen_per_maand"": 101,
    ""periode"": {
      ""start"": ""2024-12"",
      ""eind"": ""2025-02""
    },
    ""inkomen"": {
      ""loondienst"": 1565,
      ""toeslagen"": 366,
      ""overige_inkomsten"": 156,
      ""totaal"": 2322
    },
    ""uitgaven"": {
      ""vast"": 1100,
      ""variabel"": 655,
      ""vrij"": 466,
      ""totaal"": 2221
    },
    ""maandelijks_totaal"": {
      ""december_24"": 2586,
      ""januari_25"": 2645,
      ""februari_25"": 2610
    }
  },
  ""inkomensspecificatie"": {
    ""periode"": ""2025-02"",
    ""loondienst"": {
      ""sv_loon"": 1800,
      ""werkgever"": ""Werk B.V."",
      ""type_inkomen"": ""Loondienst"",
      ""type_verloning"": ""Maandelijks"",
      ""netto_inkomen"": 1565
    },
    ""uitkering"": ""nvt"",
    ""alimentatie"": ""nvt"",
    ""toeslagen"": {
      ""huurtoeslag"": 256,
      ""zorgtoeslag"": 110
    },
    ""overige_inkomsten"": {
      ""cash_storting"": 456
    }
  },
  ""uitgavenspecificatie"": {
    ""periode"": ""2025-02"",
    ""vast"": {
      ""wonen"": 1800,
      ""energie_water"": 1565,
      ""gemeentelijke_belastingen"": 1565,
      ""communicatie_media"": 1565,
      ""verzekeringen"": 1565
    },
    ""variabel"": {},
    ""vrij"": ""nvt"",
    ""overig"": {
      ""kinderopvang"": 256,
      ""vervoer"": 110,
      ""schulden_leningen"": 110,
      ""alimentatie"": 110
    },
    ""toeslagen"": {
      ""huurtoeslag"": 256,
      ""zorgtoeslag"": 110
    },
    ""overige_inkomsten"": {
      ""cash_storting"": 456
    }
  }
}

"

        IPdfGeneratorService pdfGeneratorService = new PdfGeneratorService();
        IPdfTemplate template = new TestPdfTemplate();
        pdfGeneratorService.CreatePdf(jsonData, "output.pdf", template);

        Console.WriteLine("PDF created successfully!");
    }
}
