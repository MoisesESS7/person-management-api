using PersonService.Application.Features.Persons.Responses;
using PersonService.Application.Interfaces.Repositories;
using PersonService.Application.Mappers;
using PersonService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonService.Shared.Results;

namespace PersonService.Application.Features.Persons.Commands.Create
{
    public sealed class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, ResultOfT<PersonResponse>>
    {
        private readonly ILogger<CreatePersonCommandHandler> _logger;
        private readonly IRepository<Person> _personRepository;

        public CreatePersonCommandHandler(ILogger<CreatePersonCommandHandler> logger, IRepository<Person> personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        public async Task<ResultOfT<PersonResponse>> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new person: {Name}", command.Name);
            var personFromDb = _personRepository
                .AsQueryable()
                .Where(p =>
                    p.Name == command.Name ||
                    p.Cpf.Number == command.CpfNumber ||
                    p.Rg.Number == command.RgNumber)
                .Select(p => new
                {
                    p.Name,
                    Cpf = p.Cpf.Number,
                    Rg = p.Rg.Number
                })
                .FirstOrDefault();

            if (personFromDb?.Name == command.Name)
            {
                _logger.LogWarning("There is already a person with that name: {Name}.", personFromDb.Name);
                return ResultOfT<PersonResponse>.Fail(Errors.Person.DuplicateName);
            }

            if (personFromDb?.Cpf == command.CpfNumber)
            {
                _logger.LogWarning("There is already a CPF with that number: {Number}.", personFromDb.Cpf);
                return ResultOfT<PersonResponse>.Fail(Errors.Cpf.DuplicateNumber);
            }

            if (personFromDb?.Rg == command.RgNumber)
            {
                _logger.LogWarning("There is already a RG with that number: {Number}.", personFromDb.Rg);
                return ResultOfT<PersonResponse>.Fail(Errors.Rg.DuplicateNumber);
            }

            var person = Mapper.ToPerson(command);

            var personCreated = await _personRepository.CreateAsync(person, cancellationToken);

            var personMapped = Mapper.ToPersonResponse(personCreated);

            _logger.LogInformation("Person {Name} created successfully with Id {Id}", personCreated.Name, personCreated.Id);
            return ResultOfT<PersonResponse>.Ok(personMapped);
        }
    }
}
