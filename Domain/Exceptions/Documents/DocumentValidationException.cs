using Domain.ValueObjects.Bases;

namespace Domain.Exceptions.Documents
{
    public class DocumentValidationException : DomainLayerException
    {
        public DocumentValidationException(IEnumerable<string> errors, Type? type = default) : base(errors, $"The {type?.Name ?? typeof(Document).Name} has validation errors.")
        {
        }
    }
}
