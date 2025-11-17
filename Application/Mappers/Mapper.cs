using Application.Commands.Persons;
using Application.Common.Response.Documents;
using Application.Common.Response.Persons;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Mappers
{
    public static class Mapper
    {
        public static Person ToPersonDomain<T>(T input) where T : PersonCommandBase
        {
            var cpf = new Cpf(
                input.Cpf.Number,
                input.Cpf.BirthDate,
                input.Cpf.RegistrationDate);

            var rg = new Rg(
                input.Rg.Number,
                input.Rg.BirthDate,
                input.Rg.IssuingAuthority);

            var person = new Person(
                input.Name,
                cpf,
                rg);

            return person;
        }

        public static PersonResponse ToPersonResponse(Person person)
        {
            var cpfResponse = CreateCpfResponse(person.Cpf);
            var rgResponse = CreateRgResponse(person.Rg);

            var personResponse = new PersonResponse(
                person.Id,
                person.Name,
                person.Age,
                cpfResponse,
                rgResponse);

            return personResponse;
        }

        public static IList<PersonResponse> ToListPersonResponse(IEnumerable<Person> persons)
        {
            IList<PersonResponse> personsResponse = [];

            foreach (var person in persons)
            {
                var personResponse = ToPersonResponse(person);
                personsResponse.Add(personResponse);
            }

            return personsResponse;
        }

        private static CpfResponse CreateCpfResponse(Cpf cpf)
        {
            return new CpfResponse(
                cpf.Number,
                cpf.BirthDate,
                cpf.RegistrationDate);
        }

        private static RgResponse CreateRgResponse(Rg rg)
        {
            return new RgResponse(
                rg.Number,
                rg.BirthDate,
                rg.IssuingAuthority);
        }
    }
}
