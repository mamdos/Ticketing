namespace Data.Common.Abstractions;

public abstract class BaseEntity<T> : IEntity
{
    public T Id { get; set; } = default!;
}
