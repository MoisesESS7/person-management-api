using PersonService.Application.Common.Models;
using PersonService.Application.Features.Persons.Responses;
using PersonService.Application.Interfaces.Repositories;
using PersonService.Application.Mappers;
using PersonService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonService.Shared.Results;

namespace PersonService.Application.Features.Persons.Queries.PagedSearch
{
    internal sealed class PagedSearchQueryHandler : IRequestHandler<PagedSearchQuery, ResultOfT<PagedResult<PersonResponse>>>
    {
        private readonly ILogger<PagedSearchQueryHandler> _logger;
        private readonly IRepository<Person> _personRepository;

        public PagedSearchQueryHandler(ILogger<PagedSearchQueryHandler> logger, IRepository<Person> personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        public async Task<ResultOfT<PagedResult<PersonResponse>>> Handle(PagedSearchQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all persons...");
            var totalRecords = await _personRepository.CountAsync(cancellationToken: cancellationToken);

            var pageMeta = new PageMeta(query.SearchParams.PageNumber, query.SearchParams.PageSize, totalRecords);

            var persons = await _personRepository.SeachPagedAsync(query.SearchParams, cancellationToken);

            var personList = Mapper.ToListPersonResponse(persons);

            var paginationMeta = new PaginationMeta(pageMeta);
            var pagedResult = new PagedResult<PersonResponse>(personList, paginationMeta);

            _logger.LogInformation(
                "Retrieved {Count} persons (page {Page}/{TotalPages})",
                pagedResult.Data.Count,
                query.SearchParams.PageNumber,
                pageMeta.TotalPages
            );

            return ResultOfT<PagedResult<PersonResponse>>.Ok(pagedResult);
        }
    }
}
