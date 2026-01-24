using PersonService.Domain.ValueObjects;

namespace PersonService.Domain.Exceptions.Documents
{
    public class RgValidationException : DocumentValidationException
    {
        public RgValidationException(IEnumerable<string> errors) : base(errors, typeof(Rg))
        {
        }
    }
}
