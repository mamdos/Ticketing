using Data.Common.Abstractions;

namespace Data.Entities.User.Dtos;

public record UserDto(string Id, string Email) : IDto;