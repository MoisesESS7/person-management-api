using Application.Features.Persons.Responses;
using MediatR;
using Shared.Results;

namespace Application.Features.Persons.Commands.Update
{
    public sealed class UpdatePersonCommand : IRequest<ResultOfT<PersonResponse>>
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        public UpdatePersonCommand(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
