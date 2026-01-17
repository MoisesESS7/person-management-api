using Shared.Exceptions;

namespace Domain.Exceptions.Persons
{
    public class PersonValidationException : BusinessException
    {
        public PersonValidationException(IEnumerable<string> errors) : base(errors, "The person entity has validation errors.")
        {
        }
    }
}
