namespace MFApp.Clients.Dto;

public class NipResultDto
{
    public MfResultDto? Result { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}