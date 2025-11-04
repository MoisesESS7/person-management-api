using Microsoft.AspNetCore.Http;
using Shared.Exceptions;

namespace Infrastructure.Data.Exceptions
{
    public class DatabaseConnectionException : InfrastructureLayerException
    {
        public override int StatusCode => StatusCodes.Status503ServiceUnavailable;
        public override string Title => "Database connection error";
        public override string Type => "https://httpstatuses.com/503";

        public DatabaseConnectionException() {}
        public DatabaseConnectionException(string? message) : base(message) {}
        public DatabaseConnectionException(string? message, Exception? innerException) : base(message, innerException) {}
    }
}
