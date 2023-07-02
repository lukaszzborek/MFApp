using MediatR;
using MFApp.Commands;
using MFApp.Exceptions;
using MFApp.Model;
using MFApp.Pages.Models;
using Microsoft.AspNetCore.Components;

namespace MFApp.Pages;

public partial class MF
{
    public NipSearch NipSearch { get; set; } = new();
    public string ErrorMessage { get; set; } = string.Empty;
    public NipData NipData { get; set; }
    public ILogger<MF> Logger { get; set; }

    [Inject] public IMediator Mediator { get; set; }

    private async Task SearchButtonClicked()
    {
        ErrorMessage = string.Empty;
        NipData = null;
        try
        {
            NipData = await Mediator.Send(new GetNipAndSave(NipSearch.Nip, DateOnly.FromDateTime(NipSearch.Date)));
        }
        catch (MFException e)
        {
            ErrorMessage = e.Message;
            Logger.LogError(e.ToString());
        }
        catch (Exception e)
        {
            ErrorMessage = "Wystąpił nieoczekiwany błąd";
            Logger.LogError(e.ToString());
        }
    }
}