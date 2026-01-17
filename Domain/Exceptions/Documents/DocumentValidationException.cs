using Domain.ValueObjects.Bases;
using Shared.Exceptions;

namespace Domain.Exceptions.Documents
{
    public class DocumentValidationException : BusinessException
    {
        public DocumentValidationException(IEnumerable<string> errors, Type? type = default) : base(errors, $"The {type?.Name ?? typeof(Document).Name} has validation errors.")
        {
        }
    }
}
