using System.Runtime.Serialization;

namespace ParseLib;

public class NoPropertyException : Exception
{
    public NoPropertyException()
    {
    }

    protected NoPropertyException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NoPropertyException(string? message) : base(message)
    {
    }

    public NoPropertyException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}