namespace Data.Common.Abstractions;

internal abstract class BasePolicy<TEntity> where TEntity : IEntity
{
    protected readonly TEntity _entity;

    protected BasePolicy(TEntity entity)
    {
        _entity = entity;
    }

    internal abstract void CheckConstraints();
}

internal abstract class BasePolicy<TEntity, TInput> : BasePolicy<TEntity>
    where TEntity : IEntity where TInput : IDto
{
    protected readonly TInput _input;

    protected BasePolicy(in TEntity entity,in TInput input) : base(entity)
    {
        _input = input;
    }

}
