using MFApp.Model.Enums;

namespace MFApp.Model;

public abstract class Person
{
    public string Nip { get; set; }
    public RepresentativeType Type { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CompanyName { get; set; }
    public string Pesel { get; set; }
}