using PersonService.Shared.Exceptions;

namespace PersonService.Infra.Data.Exceptions
{
    public class DatabaseConnectionException : TechnicalException
    {
        public override int StatusCode => 503;
        public override string Title => "Database connection error";
        public override string Type => "https://httpstatuses.com/503";

        public DatabaseConnectionException(string message, Exception? innerException = null) : base(message, innerException) {}
    }
}
