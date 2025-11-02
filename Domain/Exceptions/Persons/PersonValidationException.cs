namespace Domain.Exceptions.Persons
{
    public class PersonValidationException : DomainLayerException
    {
        public PersonValidationException(IEnumerable<string> errors) : base(errors, "The person entity has validation errors.")
        {
        }
    }
}
