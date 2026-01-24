namespace PersonService.Application.Features.Persons.Responses
{
    public abstract class ResponseBase
    {
        public string Id { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }

        protected ResponseBase(
            string id,
            DateTimeOffset createdAt,
            DateTimeOffset? updatedAt,
            DateTimeOffset? deletedAt)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            DeletedAt = deletedAt;
        }
    }
}
