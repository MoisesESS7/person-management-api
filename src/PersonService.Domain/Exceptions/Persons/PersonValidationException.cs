using PersonService.Shared.Exceptions;

namespace PersonService.Domain.Exceptions.Persons
{
    public class PersonValidationException : BusinessException
    {
        public PersonValidationException(IEnumerable<string> errors) : base(errors, "The person entity has validation errors.")
        {
        }
    }
}
