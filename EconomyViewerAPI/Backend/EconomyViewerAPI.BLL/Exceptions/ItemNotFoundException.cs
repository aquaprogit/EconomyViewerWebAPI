using System.Runtime.Serialization;

namespace EconomyViewerAPI.BLL.Exceptions;
public class ItemNotFoundException : Exception
{
    public ItemNotFoundException() { }

    public ItemNotFoundException(string? collectionName) : base($"Item not found in collection: {collectionName}") { }

    public ItemNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }

    protected ItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
