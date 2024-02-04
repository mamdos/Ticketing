using Data.Common.Abstractions;

namespace Data.Entities.Category.Dtos;

public record CreateCategoryDto(string Name) : IDto;