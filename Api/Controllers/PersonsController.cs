using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.Persons;
using Application.Interfaces.Services;
using Api.Requests.Persons;
using ApiResponse = Api.Responses.Persons.PersonResponse;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly ILogger<PersonsController> _logger;
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PersonsController(
            ILogger<PersonsController> logger,
            IPersonService personService,
            IMapper mapper)
        {
            _logger = logger;
            _personService = personService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetAsync([FromRoute] string id)
        {
            _logger.LogInformation("Retrieving person with ID {Id}.", id);
            var result = await _personService.GetAsync(id);
            var response = _mapper.Map<ApiResponse>(result);
            _logger.LogInformation("Person with ID {Id} retrieved successfully.", id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiResponse>>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all persons from databse.");
            var result = await _personService.GetAllAsync();
            var response = _mapper.Map<IEnumerable<ApiResponse>>(result);
            _logger.LogInformation("Retrieved {Count} persons successfully.", result.Count());

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateAsync([FromBody] CreatePersonRequest request)
        {
            _logger.LogInformation("Creating new person with name: {Name}", request.Name);
            var command = _mapper.Map<CreatePersonCommand>(request);
            var result = await _personService.CreateAsync(command);
            var response = _mapper.Map<ApiResponse>(result);
            _logger.LogInformation("Person created successfully with ID {ID}", response.Id);

            return CreatedAtAction("Get", new
            {
                response.Id
            },
            response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateAsync([FromRoute] string id, [FromBody] UpdatePersonRequest request)
        {
            _logger.LogInformation("Updating person with ID {Id}", id);
            request.SetId(id);
            var command = _mapper.Map<UpdatePersonCommand>(request);
            var result = await _personService.UpdateAsync(command);
            var response = _mapper.Map<ApiResponse>(result);
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
