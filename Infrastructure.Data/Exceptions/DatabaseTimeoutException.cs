using Microsoft.AspNetCore.Http;
using Shared.Exceptions;

namespace Infrastructure.Data.Exceptions
{
    public class DatabaseTimeoutException : InfrastructureLayerException
    {
        public override int StatusCode => StatusCodes.Status504GatewayTimeout;
        public override string Title => "Database timed out error";
        public override string Type => "https://httpstatuses.com/504";

        public DatabaseTimeoutException() { }
        public DatabaseTimeoutException(string? message) : base(message) { }
        public DatabaseTimeoutException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
