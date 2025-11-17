using Api.Models;

namespace Api.Builders
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
