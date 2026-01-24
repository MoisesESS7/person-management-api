using PersonService.Domain.Interfaces;

namespace PersonService.Domain.Entities
{
    public abstract class Entity<T> : IEntity<T>
    {
        public virtual T? Id { get; set; }
        public AuditableEntity Auditable { get; private set; } = new();

        protected Entity()
        {
            if (typeof(T) == typeof(string))
                Id = (T)(object)Guid.NewGuid().ToString();
        }
    }
}
