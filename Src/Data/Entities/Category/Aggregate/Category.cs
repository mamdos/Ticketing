using Data.Common.Abstractions;
using Data.Entities.Category.Dtos;

namespace Data.Entities.Category.Aggregate;

public class Category : BaseEntity<int>, IAggregateRoot
{
    public string Name { get; private set; } = null!;

    protected Category() { } 

    public static Category Create(CreateCategoryDto createCategoryDto)
    {

    }
}
