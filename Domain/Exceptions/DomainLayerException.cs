using Microsoft.AspNetCore.Http;
using Shared.Exceptions;

namespace Domain.Exceptions
{
    public class DomainLayerException : BaseAppException
    {
        public override int StatusCode => StatusCodes.Status400BadRequest;
        public override string Title => "Domain validation Error";
        public override string Type => "https://httpstatuses.com/400";

        public DomainLayerException() {}
        public DomainLayerException(string? message) : base(message) {}
        public DomainLayerException(IEnumerable<string> errors, string message) : base(errors, message) {}
        public DomainLayerException(string? message, Exception? innerException) : base(message, innerException) {}

    }
}
