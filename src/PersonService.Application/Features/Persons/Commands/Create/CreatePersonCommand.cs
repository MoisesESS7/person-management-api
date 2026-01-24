using PersonService.Application.Features.Persons.Responses;
using MediatR;
using PersonService.Shared.Enums;
using PersonService.Shared.Results;

namespace PersonService.Application.Features.Persons.Commands.Create
{
    public sealed class CreatePersonCommand : PersonCommandBase, IRequest<ResultOfT<PersonResponse>>
    {
        public CreatePersonCommand(
            string name,
            DateTimeOffset birthDate,
            string cpfNumber,
            DateTimeOffset cpfRegistrationDate,
            string rgNumber,
            IssuingAuthority rgIssuingAuthority)
            : base(name, birthDate, cpfNumber, cpfRegistrationDate, rgNumber, rgIssuingAuthority)
        {
        }
    }
}
