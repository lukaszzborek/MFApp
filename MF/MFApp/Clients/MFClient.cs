using System.Net;
using MFApp.Clients.Dto;
using MFApp.Kernel;

namespace MFApp.Clients;

public class MFClient : IMFClient
{
    private readonly IClock _clock;
    private readonly HttpClient _httpClient;

    public MFClient(HttpClient httpClient, IClock clock)
    {
        _httpClient = httpClient;
        _clock = clock;
    }

    public async Task<NipResultDto> GetNipResultAsync(string nip, DateOnly? date = null)
    {
        if (date == null || date == DateOnly.MinValue)
            date = DateOnly.FromDateTime(_clock.GetUtcNow());

        var response = await _httpClient.GetAsync($"api/search/nip/{nip}?date={date:yyyy-MM-dd}");
        if (response.StatusCode != HttpStatusCode.OK)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorDto>();
            var errorResult = new NipResultDto
            {
                Success = false,
                ErrorMessage = error!.Message
            };
            return errorResult;
        }

        var nipResult = await response.Content.ReadFromJsonAsync<NipResultDto>();
        nipResult!.Success = true;
        return nipResult;
    }
}