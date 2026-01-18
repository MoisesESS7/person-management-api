using Application.Features.Persons.Commands;
using Application.Features.Persons.Responses;
using Domain.Entities;

namespace Application.Mappers
{
    public static class Mapper
    {
        public static Person ToPerson<T>(T input) where T : PersonCommandBase
        {
            return Person.Create(
                input.Name,
                input.CpfNumber,
                input.BirthDate,
                input.CpfRegistrationDate,
                input.RgNumber,
                input.RgIssuingAuthority);
        }

        public static PersonResponse ToPersonResponse(Person person)
        {
            return new PersonResponse(
                person.Id ?? string.Empty,
                person.Name,
                person.Age,
                person.Rg.BirthDate,
                person.Cpf.Number,
                person.Cpf.RegistrationDate,
                person.Rg.Number,
                person.Rg.IssuingAuthority,
                person.Auditable.CreatedAt,
                person.Auditable.UpdatedAt,
                person.Auditable.DeletedAt);
        }

        public static IList<PersonResponse> ToListPersonResponse(IEnumerable<Person> persons) => [.. persons.Select(ToPersonResponse)];
    }
}
