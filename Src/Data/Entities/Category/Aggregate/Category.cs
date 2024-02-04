using Data.Common.Abstractions;
using Data.Entities.Category.Dtos;
using Data.Entities.Category.Policies;

namespace Data.Entities.Category.Aggregate;

public class Category : BaseEntity<int>, IAggregateRoot
{
    public string Name { get; private set; } = null!;

    protected Category() { }

    public static Category Create(CreateCategoryDto createCategoryDto)
    {
        Category createdCategory = new() { Name = createCategoryDto.Name };

        BasePolicy<Category, CreateCategoryDto> policy = new CategoryCreatingPolicy(createdCategory, createCategoryDto);
        policy.CheckConstraints();

        return createdCategory;
    }
}
