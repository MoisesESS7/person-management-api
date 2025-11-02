using Microsoft.AspNetCore.Http;

namespace Shared.Exceptions
{
    public class BaseAppException : Exception
    {
        public virtual int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;
        public virtual string Title { get; set; } = "Application error.";
        public virtual string Type { get; set; } = "https://httpstatuses.com/500";
        public IReadOnlyCollection<string> Errors { get; } = [];

        public BaseAppException() { }
        public BaseAppException(string? message) : base(message) { }
        public BaseAppException(string? message, Exception? innerException) : base(message, innerException) { }

        public BaseAppException(IEnumerable<string> errors, string message) : base(message)
        {
            Errors = errors != null && errors.Any()
                ? errors.ToList().AsReadOnly()
                : throw new ArgumentException("At least one error must be provided.", nameof(errors));
        }

        public override string ToString()
        {
            var details = Errors.Count > 0 ? string.Join("; ", Errors) : "No additional errors.";
            return $"{base.ToString()}\nStatusCode: {StatusCode}, Title: {Title}, Type: {Type}, Errors: {details}";
        }
    }
}
