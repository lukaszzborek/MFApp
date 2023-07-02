using MediatR;
using MFApp.Model;

namespace MFApp.Commands;

public record GetNipAndSave(string Nip, DateOnly? Date = null) : IRequest<NipData>;