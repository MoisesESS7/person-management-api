using PersonService.Shared.Exceptions;

namespace PersonService.Infra.Data.Exceptions
{
    public class DatabaseWriteException : TechnicalException
    {
        public override string Title => "Database write error";

        public DatabaseWriteException(string message, Exception? innerException = null) : base(message, innerException) { }
    }
}
