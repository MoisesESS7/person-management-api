using PersonService.Application.Features.Persons.Responses;
using PersonService.Application.Interfaces.Repositories;
using PersonService.Application.Mappers;
using PersonService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonService.Shared.Results;

namespace PersonService.Application.Features.Persons.Queries.GetById
{
    public sealed class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, ResultOfT<PersonResponse>>
    {
        private readonly ILogger<GetPersonByIdQueryHandler> _logger;
        private readonly IRepository<Person> _personRepository;

        public GetPersonByIdQueryHandler(ILogger<GetPersonByIdQueryHandler> logger, IRepository<Person> personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        public async Task<ResultOfT<PersonResponse>> Handle(GetPersonByIdQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting person with Id: {Id}", query.Id);
            var person = await _personRepository.GetAsync(p => p.Id == query.Id, cancellationToken);

            if (person is null)
            {
                _logger.LogWarning("Person with Id {Id} was not found", query.Id);
                return ResultOfT<PersonResponse>.Fail(Errors.Person.NotFound);
            }

            var personMapped = Mapper.ToPersonResponse(person);

            _logger.LogInformation("Retreved person with Id: {Id}", query.Id);
            return ResultOfT<PersonResponse>.Ok(personMapped);
        }
    }
}
