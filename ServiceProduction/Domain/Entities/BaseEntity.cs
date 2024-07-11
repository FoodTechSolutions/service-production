namespace Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = new Guid();
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdateAt { get; protected set; }
    public DateTime? DeletedAt { get; protected set; }

    protected DateTime SetCreatedAt()
    {
        return DateTime.UtcNow;
    }

    public DateTime SetUpdatedAt()
    {
        return DateTime.UtcNow;
    }
    
    public DateTime SetDeletedAt()
    {
        return DateTime.UtcNow;
    }
}