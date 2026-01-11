using Application.Commands.Persons;
using Application.Common.Response.Auditables;
using Application.Common.Response.Documents;
using Application.Common.Response.Persons;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Mappers
{
    public static class Mapper
    {
        public static Person ToPerson<T>(T input) where T : PersonCommandBase
        {
            return Person.Create(
                input.Name,
                input.Cpf.Number,
                input.Cpf.BirthDate,
                input.Cpf.RegistrationDate,
                input.Rg.Number,
                input.Rg.BirthDate,
                input.Rg.IssuingAuthority);
        }

        public static PersonResponse ToPersonResponse(Person person)
        {
            var cpfResponse = CreateCpfResponse(person.Cpf);
            var rgResponse = CreateRgResponse(person.Rg);
            var auditable = CreateAuditable(person.Auditable);

            return new (
                person.Id,
                auditable,
                person.Name,
                person.Age,
                cpfResponse,
                rgResponse);
        }

        public static IList<PersonResponse> ToListPersonResponse(IEnumerable<Person> persons) => [.. persons.Select(ToPersonResponse)];

        private static CpfResponse CreateCpfResponse(Cpf cpf) =>
            new(cpf.Number, cpf.BirthDate, cpf.RegistrationDate);

        private static RgResponse CreateRgResponse(Rg rg) =>
            new(rg.Number, rg.BirthDate, rg.IssuingAuthority);

        private static AuditableEntityResponse CreateAuditable(AuditableEntity auditable) =>
            new(auditable.CreatedAt, auditable.UpdatedAt, auditable.DeletedAt);
    }
}
