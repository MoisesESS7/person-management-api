namespace Domain.Entities
{
    public class AuditableEntity
    {
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }

        public AuditableEntity() => CreatedAt = SetOperationDate();
        public void SetUpdated() => UpdatedAt = SetOperationDate();        
        public void SetDeleted() => DeletedAt = SetOperationDate();
        private static DateTimeOffset SetOperationDate() => DateTimeOffset.UtcNow;
    }
}
