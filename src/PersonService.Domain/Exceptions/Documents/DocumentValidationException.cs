using PersonService.Domain.ValueObjects.Bases;
using PersonService.Shared.Exceptions;

namespace PersonService.Domain.Exceptions.Documents
{
    public class DocumentValidationException : BusinessException
    {
        public DocumentValidationException(IEnumerable<string> errors, Type? type = default) : base(errors, $"The {type?.Name ?? typeof(Document).Name} has validation errors.")
        {
        }
    }
}
