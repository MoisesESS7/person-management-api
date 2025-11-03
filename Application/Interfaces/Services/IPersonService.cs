using Application.Commands.Persons;
using Application.Common.Response.Persons;

namespace Application.Interfaces.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonResponse>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PersonResponse> GetAsync(string value, CancellationToken cancellationToken = default);
        Task<PersonResponse> CreateAsync(CreatePersonCommand command, CancellationToken cancellationToken = default);
        Task<PersonResponse> UpdateAsync(UpdatePersonCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string value, CancellationToken cancellationToken = default);
    }
}
