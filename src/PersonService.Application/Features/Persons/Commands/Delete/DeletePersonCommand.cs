using MediatR;
using PersonService.Shared.Results;

namespace PersonService.Application.Features.Persons.Commands.Delete
{
    public sealed class DeletePersonCommand : IRequest<Result>
    {
        public string Id { get; private set; }

        public DeletePersonCommand(string id)
        {
            Id = id ?? string.Empty;
        }
    }
}
