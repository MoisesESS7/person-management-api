namespace PersonService.Application.Common.Models
{
    public sealed class PagedResult<TEntity>
    {
        public IList<TEntity> Data { get; private set; }
        public PaginationMeta Meta { get; private set; }        

        public PagedResult(IList<TEntity> items, PaginationMeta meta)
        {
            Data = items;
            Meta = meta;
        }
    }
}
