namespace Services.User.Dtos;

public record UpdateUserRequestDto(string Id, string Email, string FirstName, string LastName, string[] Roles);