using Application.Commands.Persons;
using Application.Common.Models;
using Application.Common.Response.Persons;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Shared.Results;

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

        public async Task<ResultOfT<PagedResult<PersonResponse>>> SearchPagedAsync(SearchParams searchParams, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting all persons...");
            var totalRecords = await _personRepository.CountAsync(cancellationToken: cancellationToken);

            var pageMeta = new PageMeta(searchParams.PageNumber, searchParams.PageSize, totalRecords);

            var persons = await _personRepository.SeachPagedAsync(searchParams, cancellationToken);

            var personList = Mapper.ToListPersonResponse(persons);

            var paginationMeta = new PaginationMeta(pageMeta);
            var pagedResult = new PagedResult<PersonResponse>(personList, paginationMeta);

            _logger.LogInformation(
                "Retrieved {Count} persons (page {Page}/{TotalPages})",
                pagedResult.Data.Count,
                searchParams.PageNumber,
                pageMeta.TotalPages
            );

            return ResultOfT<PagedResult<PersonResponse>>.Ok(pagedResult);
        }

        public async Task<ResultOfT<PersonResponse>> GetAsync(string value, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting person with Id: {Id}", value);
            var person = await _personRepository.GetAsync(p => p.Id == value, cancellationToken);

            if (person is null)
            {
                _logger.LogWarning("Person with Id {Id} was not found", value);
                return ResultOfT<PersonResponse>.Fail(Errors.Person.NotFound);
            }

            var personMapped = Mapper.ToPersonResponse(person);

            _logger.LogInformation("Retreved person with Id: {Id}", value);
            return ResultOfT<PersonResponse>.Ok(personMapped);
        }

        public async Task<ResultOfT<PersonResponse>> CreateAsync(CreatePersonCommand command, CancellationToken cancellationToken = default)
        {
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
            {
                _logger.LogWarning("There is already a person with that name: {Name}.", personFromDb.Name);
                return ResultOfT<PersonResponse>.Fail(Errors.Person.DuplicateName);
            }

            if (personFromDb?.Cpf == command.Cpf.Number)
            {
                _logger.LogWarning("There is already a CPF with that number: {Number}.", personFromDb.Cpf);
                return ResultOfT<PersonResponse>.Fail(Errors.Cpf.DuplicateNumber);
            }

            if (personFromDb?.Rg == command.Rg.Number)
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

        public async Task<ResultOfT<PersonResponse>> UpdateAsync(UpdatePersonCommand command, CancellationToken cancellationToken = default)
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

        public async Task<Result> DeleteAsync(string value, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Deleting person with Id: {Id}", value);
            var exists = await _personRepository.ExistsAsync(p => p.Id == value, cancellationToken);

            if (!exists)
            {
                _logger.LogWarning("Person with Id {Id} not found", value);
                return Result.Fail(Errors.Person.NotFound);
            }

            await _personRepository.DeleteAsync(p => p.Id == value, cancellationToken);

            _logger.LogInformation("Person with Id {Id} deleted successfully", value);
            return Result.Ok();
        }
    }
}
