namespace PdfGenerator;

public class ExpenditureSpecification
{
    public string Periode { get; set; }
    public Vast Vast { get; set; }
    public Variabel Variabel { get; set; }
    public string Vrij { get; set; }
    public Overig Overig { get; set; }
    public Toeslagen Toeslagen { get; set; }
    public OverigeInkomsten OverigeInkomsten { get; set; }
}
