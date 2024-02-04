namespace Data.Common.Abstractions;

internal abstract class BasePolicy<TEntity, TInput> where TEntity : IEntity where TInput : IDto
{
    protected readonly TEntity _entity;
    protected readonly TInput _input;

    protected BasePolicy(in TEntity entity,in TInput input)
    {
        _entity = entity;
        _input = input;
    }

    internal abstract void CheckConstraints();
}
