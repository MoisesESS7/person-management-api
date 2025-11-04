using Microsoft.AspNetCore.Http;

namespace Application.Exceptions
{
    public class ResourceAlreadyExistsException : ApplicationLayerException
    {
        public override int StatusCode => StatusCodes.Status409Conflict;
        public override string Title => "Resource already exists";
        public override string Type => "https://httpstatuses.com/409";

        public ResourceAlreadyExistsException() { }
        public ResourceAlreadyExistsException(string? message) : base(message) { }
        public ResourceAlreadyExistsException(IEnumerable<string> errors, string resourceName) : base(errors, $"Has conflicts errors in create {resourceName.ToLower()}") {}
        public ResourceAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
