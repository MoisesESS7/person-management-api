using Application.Features.Persons.Responses;
using MediatR;
using Shared.Results;

namespace Application.Features.Persons.Queries.GetById
{
    public sealed class GetPersonByIdQuery : IRequest<ResultOfT<PersonResponse>>
    {
        public string Id { get; private set; }

        public GetPersonByIdQuery(string id)
        {
            Id = id ?? string.Empty;
        }
    }
}
