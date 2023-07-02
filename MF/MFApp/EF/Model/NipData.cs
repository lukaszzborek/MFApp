namespace MFApp.Model;

public class NipData
{
    public string Nip { get; set; }
    public string? Regon { get; set; }
    public DateTime? RestorationDate { get; set; }
    public string? WorkingAddress { get; set; }
    public bool HasVirtualAccounts { get; set; }
    public string? StatusVat { get; set; }
    public string? Krs { get; set; }
    public string? RestorationBasis { get; set; }
    public string? RegistrationDenialBasis { get; set; }
    public DateTime? RemovalDate { get; set; }
    public string? Name { get; set; }
    public DateTime? RegistrationLegalDate { get; set; }
    public string? RemovalBasis { get; set; }
    public string? Pesel { get; set; }
    public string? ResidenceAddress { get; set; }
    public DateTime? RegistrationDenialDate { get; set; }
    public List<NipAccountNumber> AccountNumbers { get; set; } = new();
    public List<Partner> Partners { get; set; } = new();
    public List<AuthorizedClerk> AuthorizedClerks { get; set; } = new();
    public List<Representative> Representatives { get; set; } = new();
    public DateTime LastUpdateDate { get; set; }
}