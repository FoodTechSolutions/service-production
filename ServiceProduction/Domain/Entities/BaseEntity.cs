namespace Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = new Guid();
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdateAt { get; protected set; }

    protected DateTime SetCreatedAt()
    {
        return DateTime.UtcNow;
    }

    protected DateTime SetUpdatedAt()
    {
        return DateTime.UtcNow;
    }
}