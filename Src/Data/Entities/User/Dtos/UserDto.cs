using Data.Common.Abstractions;

namespace Data.Entities.User.Dtos;

public record UserDto(string Id, string Email, string UserName, string FirstName, string LastName) : IDto;