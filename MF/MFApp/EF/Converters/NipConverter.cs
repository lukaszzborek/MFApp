using MFApp.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MFApp.EF.Converters;

public class NipConverter : ValueConverter<Nip, string>
{
    public NipConverter()
        : base(x => x.Value, x => new Nip(x))
    {
    }
}