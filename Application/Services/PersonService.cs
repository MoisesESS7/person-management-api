using Application.Commands.Persons;
using Application.Common.Response.Persons;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly ILogger<PersonService> _logger;
        private readonly IRepository<Person> _personRepository;

        public PersonService(
            ILogger<PersonService> logger,
            IRepository<Person> personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting all persons...");
            var persons = await _personRepository.GetAllAsync(cancellationToken);
            
            var response = Mapper.ToListPersonResponse(persons)
                                 .OrderBy(p => p.Name)
                                 .ToList();

            _logger.LogInformation("Retrieved {Count} persons", response.Count);

            return response;
        }

        public async Task<PersonResponse> GetAsync(string value, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting person with Id: {Id}", value);
            var person = await _personRepository.GetAsync(p => p.Id == value, cancellationToken);

            if (person is null)
            {
                _logger.LogWarning("Person with Id {Id} not found", value);
                throw new ResourceNotFoundException(nameof(Person), value);
            }

            _logger.LogInformation("Person with Id {Id} found", value);
            return Mapper.ToPersonResponse(person);
        }

        public async Task<PersonResponse> CreateAsync(CreatePersonCommand command, CancellationToken cancellationToken = default)
        {
            IList<string> errors = [];

            _logger.LogInformation("Creating a new person: {Name}", command.Name);
            var personFromDb = _personRepository
                .AsQueryable()
                .Where(p =>
                    p.Name == command.Name ||
                    p.Cpf.Number == command.Cpf.Number ||
                    p.Rg.Number == command.Rg.Number)
                .Select(p => new
                {
                    p.Name,
                    Cpf = p.Cpf.Number,
                    Rg = p.Rg.Number
                })
                .FirstOrDefault();

            if (personFromDb?.Name == command.Name)
                errors.Add($"There is already a person with that name");

            if (personFromDb?.Cpf == command.Cpf.Number)
                errors.Add($"There is alread a CPF with that number");

            if (personFromDb?.Rg == command.Rg.Number)
                errors.Add($"There is already a RG with that number");

            if (errors.Count > 0)
            {
                _logger.LogWarning("Failed to create person {Name}. Errors: {Errors}", command.Name, string.Join(", ", errors));
                throw new ResourceAlreadyExistsException(errors, nameof(Person));
            }

            var person = Mapper.ToPersonDomain(command);
            var personCreated = await _personRepository.CreateAsync(person, cancellationToken);
            _logger.LogInformation("Person {Name} created successfully with Id {Id}", personCreated.Name, personCreated.Id);
            return Mapper.ToPersonResponse(personCreated);
        }

        public async Task<PersonResponse> UpdateAsync(UpdatePersonCommand command, CancellationToken cancellationToken = default)
        {
            IList<string> errors = [];

            _logger.LogInformation("Updating person with Id: {Id}", command.Id);
            var person = await _personRepository.GetAsync(p => p.Id == command.Id, cancellationToken);

            if (person is null)
            {
                _logger.LogWarning("Person with Id {Id} not found", command.Id);
                throw new ResourceNotFoundException(nameof(Person), command.Id);
            }

            if (person.Name == command.Name)
                errors.Add($"There is already a person with that name");

            if (errors.Count > 0)
            {
                _logger.LogWarning("Failed to update person with Id {Id}. Errors: {Errors}", command.Id, string.Join(", ", errors));
                throw new ResourceAlreadyExistsException(errors, nameof(Person));
            }

            person.Update(command.Name);
            var personUpdated = await _personRepository.UpdateAsync(person, cancellationToken);
            _logger.LogInformation("Person with Id {Id} updated successfully", command.Id);
            return Mapper.ToPersonResponse(personUpdated);
        }

        public async Task<bool> DeleteAsync(string value, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Deleting person with Id: {Id}", value);
            var exists = await _personRepository.ExistsAsync(p => p.Id == value, cancellationToken);

            if (!exists)
            {
                _logger.LogWarning("Person with Id {Id} not found", value);
                throw new ResourceNotFoundException(nameof(Person), value);
            }

            var isDeleted = await _personRepository.DeleteAsync(p => p.Id == value, cancellationToken);
            _logger.LogInformation("Person with Id {Id} deleted successfully", value);
            return isDeleted;
        }
    }
}
