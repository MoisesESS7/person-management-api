using Shared.Exceptions;

namespace Infrastructure.Data.Exceptions
{
    public class DatabaseWriteException : InfrastructureLayerException
    {
        public override string Title => "Database write error";

        public DatabaseWriteException() { }
        public DatabaseWriteException(string? message) : base(message) { }
        public DatabaseWriteException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
