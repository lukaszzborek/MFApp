namespace MFApp.Exceptions;

public class FailedToGetNipDataException : MFException
{
    public string Message { get; }

    public FailedToGetNipDataException(string message) : base(message)
    {
        Message = message;
    }
}

public class MFException : Exception
{
    public MFException(string message) : base(message)
    {
    }
}