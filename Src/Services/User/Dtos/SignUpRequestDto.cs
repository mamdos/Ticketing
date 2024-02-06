namespace Services.User.Dtos;

public record SignUpRequestDto(
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    string Password);