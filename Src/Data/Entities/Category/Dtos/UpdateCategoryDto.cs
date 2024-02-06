using Data.Common.Abstractions;

namespace Data.Entities.Category.Dtos;

public record UpdateCategoryDto(string Name) : IDto;