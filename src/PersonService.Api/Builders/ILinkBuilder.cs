using PersonService.Api.Models;

namespace PersonService.Api.Builders
{
    public interface ILinkBuilder
    {
        PaginationLinks Build(
            string actionName,
            int pageNumber,
            int pageSize,
            int totalPages);
    }
}
