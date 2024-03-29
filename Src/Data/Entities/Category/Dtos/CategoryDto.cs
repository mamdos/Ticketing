﻿using Data.Common.Abstractions;

namespace Data.Entities.Category.Dtos;

public record CategoryDto(int Id, string Name) : IDto;