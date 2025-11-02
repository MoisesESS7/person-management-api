using Domain.ValueObjects;

namespace Domain.Exceptions.Documents
{
    public class CpfValidationException : DocumentValidationException
    {
        public CpfValidationException(IEnumerable<string> errors) : base(errors, typeof(Cpf))
        {
        }
    }
}
