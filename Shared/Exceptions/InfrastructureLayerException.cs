using Microsoft.AspNetCore.Http;

namespace Shared.Exceptions
{
    public class InfrastructureLayerException : BaseAppException
    {
        public override int StatusCode => StatusCodes.Status500InternalServerError;
        public override string Title => "Infrastructure error.";
        public override string Type => "https://httpstatuses.com/500";

        public InfrastructureLayerException() { }
        public InfrastructureLayerException(string? message) : base(message) { }
        public InfrastructureLayerException(IEnumerable<string> errors, string message) : base(errors , message) {}
        public InfrastructureLayerException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
