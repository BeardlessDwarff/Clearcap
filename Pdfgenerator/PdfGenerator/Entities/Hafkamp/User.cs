using PdfGenerator.Entities.Hafkamp;

namespace PdfGenerator;

public class User
{
    public Identity Identity { get; set; }
    public ContactDetails ContactDetails { get; set; }
    public Address Address { get; set; }
    public Household Household { get; set; }
}
