namespace Shared.Exceptions
{
    public abstract class AppException : Exception
    {
        public virtual int StatusCode => 500;
        public virtual string Title => "Application error.";
        public virtual string Type => "https://httpstatuses.com/500";
        public IReadOnlyCollection<string> Errors { get; }

        protected AppException(string message) : base(message) => Errors = [];
        protected AppException(string? message, Exception? innerException) : base(message, innerException) => Errors = [];
        protected AppException(IEnumerable<string> errors, string message) : base(message) =>
            Errors = errors != null && errors.Any() ? errors.ToList().AsReadOnly() : [];
    }
}
