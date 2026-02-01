using PersonService.Application.Features.Persons.Commands.Create;
using PersonService.Shared.Enums;

namespace PersonService.Tests.Unit.Application.Features.Persons.Commands.Create
{
    public sealed class CreatePersonCommandBuilder
    {
        private string _name = "John Doe";
        private DateTimeOffset _birthDate = new(new DateTime(1990, 10, 10));
        private string _cpfNumber = "12345678910";
        private DateTimeOffset _cpfRegistrationDate = new(new DateTime(1990, 11, 11));
        private string _rgNumber = "123456789";
        private IssuingAuthority _rgIssuingAuthority = IssuingAuthority.SSP_SP;

        public CreatePersonCommandBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CreatePersonCommandBuilder WithCpfNumber(string number)
        {
            _cpfNumber = number;
            return this;
        }

        public CreatePersonCommandBuilder WithRgNumber(string number)
        {
            _rgNumber = number;
            return this;
        }

        public CreatePersonCommandBuilder WithBirthDate(DateTimeOffset birthDate)
        {
            _birthDate = birthDate;
            return this;
        }

        public CreatePersonCommandBuilder WithCpfRegistrationDate(DateTimeOffset date)
        {
            _cpfRegistrationDate = date;
            return this;
        }

        public CreatePersonCommandBuilder WithIssuingAuthority(IssuingAuthority authority)
        {
            _rgIssuingAuthority = authority;
            return this;
        }

        public CreatePersonCommand Build() =>
            new(
                _name,
                _birthDate,
                _cpfNumber,
                _cpfRegistrationDate,
                _rgNumber,
                _rgIssuingAuthority
            );
    }
}
