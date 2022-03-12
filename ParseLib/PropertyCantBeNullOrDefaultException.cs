using System.Runtime.Serialization;

namespace ParseLib;

public class PropertyCantBeNullOrDefaultException : Exception
{
    public PropertyCantBeNullOrDefaultException()
    {
    }

    protected PropertyCantBeNullOrDefaultException(SerializationInfo info, StreamingContext context) : base(
        info,
        context)
    {
    }

    public PropertyCantBeNullOrDefaultException(string? message) : base(message)
    {
    }

    public PropertyCantBeNullOrDefaultException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}