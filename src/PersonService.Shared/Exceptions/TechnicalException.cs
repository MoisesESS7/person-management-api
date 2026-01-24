namespace PersonService.Shared.Exceptions
{
    public class TechnicalException : AppException
    {
        public override string Title => "Technical error.";

        public TechnicalException(string message, Exception? innerException = null) : base(message, innerException) { }
    }
}
