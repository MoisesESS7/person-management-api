using Domain.ValueObjects;

namespace Domain.Exceptions.Documents
{
    public class RgValidationException : DocumentValidationException
    {
        public RgValidationException(IEnumerable<string> errors) : base(errors, typeof(Rg))
        {
        }
    }
}
