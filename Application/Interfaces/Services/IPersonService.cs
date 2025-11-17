using Application.Commands.Persons;
using Application.Common.Models;
using Application.Common.Response.Persons;

namespace Application.Interfaces.Services
{
    public interface IPersonService
    {
        Task<PagedResult<PersonResponse>> SearchPagedAsync(SearchParams searchParams, CancellationToken cancellationToken = default);
        Task<PersonResponse> GetAsync(string value, CancellationToken cancellationToken = default);
        Task<PersonResponse> CreateAsync(CreatePersonCommand command, CancellationToken cancellationToken = default);
        Task<PersonResponse> UpdateAsync(UpdatePersonCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string value, CancellationToken cancellationToken = default);
    }
}
