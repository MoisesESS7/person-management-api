using Microsoft.AspNetCore.Http;

namespace Application.Exceptions
{
    public class ResourceNotFoundException : ApplicationLayerException
    {
        public override int StatusCode => StatusCodes.Status404NotFound;
        public override string Title => "Resource was not found";
        public override string Type => "https://httpstatuses.com/404";

        public ResourceNotFoundException() {}
        public ResourceNotFoundException(string? message) : base(message) {}
        public ResourceNotFoundException(string resourceName, string resourceId) : base($"{resourceName} with ID: {resourceId} was not found.") {}
        public ResourceNotFoundException(string? message, Exception? innerException) : base(message, innerException) {}
    }
}
