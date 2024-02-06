using Data.Common.Abstractions;
using Data.Common.Exceptions;
using Data.Entities.Category.Dtos;

namespace Data.Entities.Category.Policies;

internal class CategoryUpdatingPolicy : BasePolicy<Aggregate.Category, UpdateCategoryDto>
{
    internal CategoryUpdatingPolicy(in Aggregate.Category entity, in UpdateCategoryDto input) : base(entity, input)
    {
    }

    internal override void CheckConstraints()
    {

    }

    private void CheckIfFieldsAreFilled()
    {
        if (string.IsNullOrWhiteSpace(_input.Name))
            throw new InvalidEntityStateException("all the required fields must be filled with data");
    }
}
