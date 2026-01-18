using System.Text.Json.Serialization;

namespace Api.Models
{
    public sealed class PagedResponse<TInput>
    {
        [JsonPropertyOrder(1)]
        public List<TInput> Data { get; private set; }

        [JsonPropertyOrder(2)]
        public PaginationLinks? Links { get; private set; }

        [JsonPropertyOrder(3)]
        public PaginationMeta Meta { get; private set; }

        public PagedResponse(List<TInput> data, PaginationMeta meta)
        {
            Data = data;
            Meta = meta;
        }

        public void SetLinks(PaginationLinks links)
        {
            Links = links;
        }
    }
}
