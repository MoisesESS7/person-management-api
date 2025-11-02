namespace Domain.Entities
{
    public class AuditableEntity
    {
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public AuditableEntity() => CreatedAt = SetOperationDate();
        public void SetUpdated() => UpdatedAt = SetOperationDate();        
        public void SetDeleted() => DeletedAt = SetOperationDate();
        private static DateTime SetOperationDate() => DateTime.UtcNow;
    }
}
