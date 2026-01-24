using Shared.Exceptions;

namespace Infrastructure.Data.Exceptions
{
    public class DatabaseTimeoutException : TechnicalException
    {
        public override int StatusCode => 504;
        public override string Title => "Database timed out error";
        public override string Type => "https://httpstatuses.com/504";

        public DatabaseTimeoutException(string message, Exception? innerException = null) : base(message, innerException) { }
    }
}
