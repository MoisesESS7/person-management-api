namespace Shared.Exceptions
{
    public class InfrastructureLayerException : BaseAppException
    {
        public override string Title => "Infrastructure error.";

        public InfrastructureLayerException() { }
        public InfrastructureLayerException(string? message) : base(message) { }
        public InfrastructureLayerException(IEnumerable<string> errors, string message) : base(errors , message) {}
        public InfrastructureLayerException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
