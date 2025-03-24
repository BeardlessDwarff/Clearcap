using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PdfGenerator.PdfGeneration.Templates.HafkampTemplateTest1;

namespace PdfGenerator;

class Program
{
    static void Main()
    {
        string peopleJsonPath = @"..\..\TestData\people.json";
        string jsonDataHafkampPath = @"..\..\TestData\hafkampdatatest1.json";

        string peopleJson = File.ReadAllText(peopleJsonPath);
        string jsonDataHafkamp = File.ReadAllText(jsonDataHafkampPath);

        var dataHafkamp = JsonConvert.DeserializeObject<DataHafkamp>(jsonDataHafkamp);

        IPdfGeneratorService pdfGeneratorService = new PdfGeneratorService();

        IPdfTemplate hafkampTemplate = new HafkampTemplateTest1();
        pdfGeneratorService.CreatePdf(jsonDataHafkamp, "hafkampTestData1.pdf", hafkampTemplate);

        //IPdfTemplate template = new TestPdfTemplate();
        //pdfGeneratorService.CreatePdf(peopleJson, "peopleTestData.pdf", template);



        Console.WriteLine("PDF created successfully!");
    }
}
