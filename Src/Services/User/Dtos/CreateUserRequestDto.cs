namespace Services.User.Dtos;

public record CreateUserRequestDto(
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    string Password,
    string[] Roles);