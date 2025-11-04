using Microsoft.AspNetCore.Http;
using Shared.Exceptions;

namespace Application.Exceptions
{
    public class ApplicationLayerException : BaseAppException
    {
        public override int StatusCode => StatusCodes.Status422UnprocessableEntity;
        public override string Title => "Application process error";
        public override string Type => "https://httpstatuses.com/422";

        public ApplicationLayerException() {}
        public ApplicationLayerException(string? message) : base(message) {}
        public ApplicationLayerException(IEnumerable<string> errors, string message) : base (errors, message) {}
        public ApplicationLayerException(string? message, Exception? innerException) : base(message, innerException) {}
    }
}
