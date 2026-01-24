using PersonService.Domain.ValueObjects;

namespace PersonService.Domain.Exceptions.Documents
{
    public class CpfValidationException : DocumentValidationException
    {
        public CpfValidationException(IEnumerable<string> errors) : base(errors, typeof(Cpf))
        {
        }
    }
}
