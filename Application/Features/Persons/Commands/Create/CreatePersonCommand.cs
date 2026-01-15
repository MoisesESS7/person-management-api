using Application.Features.Persons.Responses;
using MediatR;
using Shared.Enums;
using Shared.Results;

namespace Application.Features.Persons.Commands.Create
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
