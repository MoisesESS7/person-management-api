using Shared.Exceptions;

namespace Infrastructure.Data.Exceptions
{
    public class DatabaseConnectionException : BaseAppException
    {
        public override int StatusCode => 503;
        public override string Title => "Database connection error";
        public override string Type => "https://httpstatuses.com/503";

        public DatabaseConnectionException() {}
        public DatabaseConnectionException(string? message) : base(message) {}
        public DatabaseConnectionException(string? message, Exception? innerException) : base(message, innerException) {}
    }
}
