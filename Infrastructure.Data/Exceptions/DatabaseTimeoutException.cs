using Shared.Exceptions;

namespace Infrastructure.Data.Exceptions
{
    public class DatabaseTimeoutException : BaseAppException
    {
        public override int StatusCode => 504;
        public override string Title => "Database timed out error";
        public override string Type => "https://httpstatuses.com/504";

        public DatabaseTimeoutException() { }
        public DatabaseTimeoutException(string? message) : base(message) { }
        public DatabaseTimeoutException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
