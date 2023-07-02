namespace MFApp.Kernel;

public class Clock : IClock
{
    public DateTime GetUtcNow()
    {
        return DateTime.UtcNow;
    }
}