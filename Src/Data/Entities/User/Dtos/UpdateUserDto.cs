using Data.Common.Abstractions;

namespace Data.Entities.User.Dtos;

public record UpdateUserDto(string Email, string FirstName, string LastName) : IDto;