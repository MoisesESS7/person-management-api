using PersonService.Domain.Entities;
using PersonService.Shared.Enums;

namespace PersonService.Tests.Common.Builders
{
    public sealed class PersonBuilder
    {
        private string _name = "John Doe";
        private DateTimeOffset _birthDate = new(new DateTime(1990, 10, 10));
        private string _cpfNumber = "12345678910";
        private DateTimeOffset _cpfRegistrationDate = new(new DateTime(1990, 11, 11));
        private string _rgNumber = "123456789";
        private IssuingAuthority _rgIssuingAuthority = IssuingAuthority.SSP_SP;

        public PersonBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public PersonBuilder WithCpfNumber(string number)
        {
            _cpfNumber = number;
            return this;
        }

        public PersonBuilder WithRgNumber(string number)
        {
            _rgNumber = number;
            return this;
        }

        public PersonBuilder WithBirthDate(DateTimeOffset birthDate)
        {
            _birthDate = birthDate;
            return this;
        }

        public PersonBuilder WithCpfRegistrationDate(DateTimeOffset date)
        {
            _cpfRegistrationDate = date;
            return this;
        }

        public PersonBuilder WithIssuingAuthority(IssuingAuthority authority)
        {
            _rgIssuingAuthority = authority;
            return this;
        }

        public Person Build() =>
            Person.Create(
                _name,
                _cpfNumber,
                _birthDate,
                _cpfRegistrationDate,
                _rgNumber,
                _rgIssuingAuthority);
    }
}
