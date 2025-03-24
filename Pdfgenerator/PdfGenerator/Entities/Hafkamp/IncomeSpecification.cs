namespace PdfGenerator;

public class IncomeSpecification
{
    public string Periode { get; set; }
    public Loondienst Loondienst { get; set; }
    public string Uitkering { get; set; }
    public string Alimentatie { get; set; }
    public Toeslagen Toeslagen { get; set; }
    public OverigeInkomsten OverigeInkomsten { get; set; }
}
