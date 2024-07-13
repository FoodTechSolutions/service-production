namespace Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = new Guid();
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
    public DateTime? DeletedAt { get; protected set; }

    protected BaseEntity SetCreatedAt()
    {
        CreatedAt = DateTime.UtcNow;
        return this;
    }

    public BaseEntity SetUpdatedAt()
    {
        UpdatedAt = DateTime.UtcNow;
        return this;
    }
    
    public BaseEntity SetDeletedAt()
    {
        DeletedAt = DateTime.UtcNow;
        return this;
    }
}