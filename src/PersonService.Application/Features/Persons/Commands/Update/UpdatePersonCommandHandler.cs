using PersonService.Application.Features.Persons.Responses;
using PersonService.Application.Interfaces.Repositories;
using PersonService.Application.Mappers;
using PersonService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonService.Shared.Results;

namespace PersonService.Application.Features.Persons.Commands.Update
{
    public sealed class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, ResultOfT<PersonResponse>>
    {
        private readonly ILogger<UpdatePersonCommandHandler> _logger;
        private readonly IRepository<Person> _personRepository;

        public UpdatePersonCommandHandler(ILogger<UpdatePersonCommandHandler> logger, IRepository<Person> personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        public async Task<ResultOfT<PersonResponse>> Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating person with Id: {Id}", command.Id);
            var exists = await _personRepository.ExistsAsync(p => p.Name == command.Name, cancellationToken);

            if (exists)
            {
                _logger.LogWarning("There is already a person with that name: {Name}.", command.Name);
                return ResultOfT<PersonResponse>.Fail(Errors.Person.DuplicateName);
            }

            var person = await _personRepository.GetAsync(p => p.Id == command.Id, cancellationToken);

            if (person is null)
            {
                _logger.LogWarning("Person with Id {Id} not found", command.Id);
                return ResultOfT<PersonResponse>.Fail(Errors.Person.NotFound);
            }

            person.Update(command.Name);

            var personUpdated = await _personRepository.UpdateAsync(person, cancellationToken);

            var personMapped = Mapper.ToPersonResponse(personUpdated);

            _logger.LogInformation("Person with Id {Id} updated successfully", command.Id);
            return ResultOfT<PersonResponse>.Ok(personMapped);
        }
    }
}
