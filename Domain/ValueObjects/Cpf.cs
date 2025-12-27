using Domain.Exceptions.Documents;
using Domain.ValueObjects.Bases;

namespace Domain.ValueObjects
{
    public sealed class Cpf : Document
    {
        public DateTimeOffset RegistrationDate { get; private set; }

        public Cpf(string number, DateTimeOffset birthDate, DateTimeOffset registrationDate) : base(number, birthDate)
        {
            RegistrationDate = registrationDate;
            Validation();
        }

        private void Validation()
        {
            var errors = new List<string>();

            if (Number.Length != 11)
                errors.Add("The CPF number can be equal 11 digits.");            
            
            if (RegistrationDate.Date < BirthDate.Date)
                errors.Add("The registration date cannot be earlier than the date of birth.");

            if(errors.Count > 0)
                throw new CpfValidationException(errors);
        }
    }
}
