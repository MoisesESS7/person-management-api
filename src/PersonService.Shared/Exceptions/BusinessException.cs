namespace PersonService.Shared.Exceptions
{
    public abstract class BusinessException : AppException
    {
        public override int StatusCode => 400;
        public override string Title => "Business error.";
        public override string Type => "https://httpstatuses.com/400";

        protected BusinessException(string message) : base(message) { }
        protected BusinessException(IEnumerable<string> errors, string message) : base(errors, message) { }
    }
}
