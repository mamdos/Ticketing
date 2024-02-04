using Data.Common.Abstractions;
using Data.Common.Exceptions;
using Data.Entities.Category.Dtos;

namespace Data.Entities.Category.Policies;

internal class CategoryCreatingPolicy : BasePolicy<Aggregate.Category, CreateCategoryDto>
{
    internal CategoryCreatingPolicy(in Aggregate.Category entity, in CreateCategoryDto input) : base(entity, input)
    {
    }

    internal override void CheckConstraints()
    {
        CheckInputsAreFilled();
    }

    private void CheckInputsAreFilled()
    {
        if (string.IsNullOrWhiteSpace(_input.Name))
            throw new InvalidEntityStateException("required inputs should be filled");
    }
}
