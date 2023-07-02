namespace MFApp.Clients.Dto;

public class Subject
{
    public RepresentativesDto[] AuthorizedClerks { get; set; }
    public string Regon { get; set; }
    public DateTime? RestorationDate { get; set; }
    public string WorkingAddress { get; set; }
    public bool HasVirtualAccounts { get; set; }
    public string StatusVat { get; set; }
    public string Krs { get; set; }
    public string RestorationBasis { get; set; }
    public string[] AccountNumbers { get; set; }
    public string RegistrationDenialBasis { get; set; }
    public string Nip { get; set; }
    public DateTime? RemovalDate { get; set; }
    public RepresentativesDto[] Partners { get; set; }
    public string Name { get; set; }
    public DateTime? RegistrationLegalDate { get; set; }
    public string RemovalBasis { get; set; }
    public string Pesel { get; set; }
    public RepresentativesDto[] Representatives { get; set; }
    public string ResidenceAddress { get; set; }
    public DateTime? RegistrationDenialDate { get; set; }
}