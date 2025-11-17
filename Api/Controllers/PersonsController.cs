using Api.Builders;
using Api.Models;
using Api.Requests.Persons;
using Application.Commands.Persons;
using Application.Common.Models;
using Application.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApiPersonResponse = Api.Responses.Persons.PersonResponse;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly ILogger<PersonsController> _logger;
        private readonly ILinkBuilder _linkBuilder;
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PersonsController(
            ILogger<PersonsController> logger,
            IPersonService personService,
            IMapper mapper,
            ILinkBuilder linkBuilder)
        {
            _logger = logger;
            _personService = personService;
            _mapper = mapper;
            _linkBuilder = linkBuilder;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiPersonResponse>> GetAsync([FromRoute] string id)
        {
            _logger.LogInformation("Retrieving person with ID {Id}.", id);
            var result = await _personService.GetAsync(id);
            var response = _mapper.Map<ApiPersonResponse>(result);
            _logger.LogInformation("Person with ID {Id} retrieved successfully.", id);

            return Ok(response);
        }

        [HttpGet(Name = "Paged")]
        public async Task<ActionResult<PagedResponse<ApiPersonResponse>>> GetPagedAsync(SearchParamsQuery query)
        {
            _logger.LogInformation("Retrieving all persons from databse.");

            var searchParams = new SearchParams
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                SearchTerm = query.SearchTerm,
                SortBy = query.SortBy,
                SortDescending = query.SortDescending
            };
            
            var result = await _personService.SearchPagedAsync(searchParams);

            var response = _mapper.Map<PagedResponse<ApiPersonResponse>>(result);

            var links = _linkBuilder.Build(
                "Paged",
                searchParams.PageNumber,
                searchParams.PageSize,
                result.Meta.PageMeta?.TotalPages ?? 0
            );

            response.SetLinks(links);

            _logger.LogInformation(
                "Retrieved {Count} persons (page {Page}/{TotalPages})",
                response.Data.Count,
                searchParams.PageNumber,
                result.Meta.PageMeta?.TotalPages ?? 0
            );
            
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiPersonResponse>> CreateAsync([FromBody] CreatePersonRequest request)
        {
            _logger.LogInformation("Creating new person with name: {Name}", request.Name);
            var command = _mapper.Map<CreatePersonCommand>(request);
            var result = await _personService.CreateAsync(command);
            var response = _mapper.Map<ApiPersonResponse>(result);
            _logger.LogInformation("Person created successfully with ID {ID}", response.Id);

            return CreatedAtAction("Get", new
            {
                response.Id
            },
            response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiPersonResponse>> UpdateAsync([FromRoute] string id, [FromBody] UpdatePersonRequest request)
        {
            _logger.LogInformation("Updating person with ID {Id}", id);
            request.SetId(id);
            var command = _mapper.Map<UpdatePersonCommand>(request);
            var result = await _personService.UpdateAsync(command);
            var response = _mapper.Map<ApiPersonResponse>(result);
            _logger.LogInformation("Person with ID {Id} updated successfully.", id);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] string id)
        {
            _logger.LogInformation("Deleting person with ID {Id}", id);
            await _personService.DeleteAsync(id);
            _logger.LogInformation("Person with ID {Id} deleted successfully.", id);

            return NoContent();
        }
    }
}
