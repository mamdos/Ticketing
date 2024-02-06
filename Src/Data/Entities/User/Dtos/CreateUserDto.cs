using Data.Common.Abstractions;

namespace Data.Entities.User.Dtos;

public record CreateUserDto(string UserName, string Email, string FirstName, string LastName) : IDto;