namespace Domain.Common
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; }

        public void SetUpdatedNow() => UpdatedAt = DateTime.UtcNow;
    }
}
