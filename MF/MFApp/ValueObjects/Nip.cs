namespace MFApp.ValueObjects;

public record Nip(string Value)
{
    public static implicit operator string(Nip nip)
    {
        return nip.Value;
    }

    public static implicit operator Nip(string assetId)
    {
        return new Nip(assetId);
    }
}