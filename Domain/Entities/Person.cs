using Domain.Attributes;
using Domain.Exceptions.Persons;
using Domain.ValueObjects;
using Shared.Enums;

namespace Domain.Entities
{
    [BsonCollection("persons")]
    public class Person : Entity<string>
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public Cpf Cpf { get; private set; }
        public Rg Rg { get; private set; }

        private Person(string name, Cpf cpf, Rg rg)
        {
            Name = name;
            Rg = rg;
            Cpf = cpf;
            CalculeteAge(rg.BirthDate);
            Validation();
        }

        public static Person Create(
            string name,
            string cpfNumber,
            DateTimeOffset birthDate,
            DateTimeOffset cpfRegistrationDate,
            string rgNumber,
            IssuingAuthority issuingAuthority)
        {
            var cpf = new Cpf(cpfNumber, birthDate, cpfRegistrationDate);
            var rg = new Rg(rgNumber, birthDate, issuingAuthority);

            return new Person(name, cpf, rg);
        }

        private void CalculeteAge(DateTimeOffset birthDate) => Age = DateTimeOffset.Now.Year - birthDate.Year;

        public void Update(string name)
        {
            Name = name;
            Auditable.SetUpdated();
            PartialValidation();
        }

        private void Validation()
        {
            var errors = new List<string>();
            
            if (Age < 18)
                errors.Add("Age cannot be less than 18 years.");
            
            if (Age > 70)
                errors.Add("Age cannot be greater than 70 years.");

            PartialValidation(errors);
        }

        private void PartialValidation(List<string>? errors = default)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new PersonValidationException(["The name can be informed."]);
            
            if (Name?.Length < 3)
                errors?.Add("The name cannot be less then 3 characters.");
            
            if (Name?.Length > 100)
                errors?.Add("The name cannot be greater then 100 characters.");

            if (errors?.Count > 0)
                throw new PersonValidationException(errors);
        }
    }
}
