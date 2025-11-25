namespace Parentaliza.Domain.Entidades;
public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    internal void SetId(Guid id)
    {
        Id = id;
    }

    internal void SetCreatedAt()
    {
        if (CreatedAt == default)
        {
            CreatedAt = DateTime.UtcNow;
        }
    }

    internal void SetUpdatedAt()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}