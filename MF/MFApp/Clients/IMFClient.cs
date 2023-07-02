using MFApp.Clients.Dto;

namespace MFApp.Clients;

public interface IMFClient
{
    Task<NipResultDto> GetNipResultAsync(string nip, DateOnly? date = null);
}