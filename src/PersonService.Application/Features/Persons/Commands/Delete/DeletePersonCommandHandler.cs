using PersonService.Application.Interfaces.Repositories;
using PersonService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonService.Shared.Results;

namespace PersonService.Application.Features.Persons.Commands.Delete
{
    public sealed class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Result>
    {
        private readonly ILogger<DeletePersonCommandHandler> _logger;
        private readonly IRepository<Person> _personRepository;

        public DeletePersonCommandHandler(ILogger<DeletePersonCommandHandler> logger, IRepository<Person> personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        public async Task<Result> Handle(DeletePersonCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting person with Id: {Id}", command.Id);
            var exists = await _personRepository.ExistsAsync(p => p.Id == command.Id, cancellationToken);

            if (!exists)
            {
                _logger.LogWarning("Person with Id {Id} not found", command.Id);
                return Result.Fail(Errors.Person.NotFound);
            }

            await _personRepository.DeleteAsync(p => p.Id == command.Id, cancellationToken);

            _logger.LogInformation("Person with Id {Id} deleted successfully", command.Id);
            return Result.Ok();
        }
    }
}
