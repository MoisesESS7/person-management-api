namespace Application.Exceptions
{
    public class PageNotFoundException : ApplicationLayerException
    {
        public override string Title => "Page not found";

        public PageNotFoundException() : base("The requested page does not exist.") {}
        public PageNotFoundException(string? message) : base(message) {}
        public PageNotFoundException(IEnumerable<string> errors, string message) : base(errors, message) {}
        public PageNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
