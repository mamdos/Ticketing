using Data.Common.Abstractions;
using Data.Entities.User.Dtos;
using Data.Entities.User.Policies;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities.User.Aggregate;

public class User : IdentityUser, IAggregateRoot, IEntity
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;

    protected User()
    { }

    public static User Create(CreateUserDto createUserDto)
    {
        var user = new User
        {
            UserName = createUserDto.UserName,
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            Email = createUserDto.Email,
            EmailConfirmed = true
        };

        var policy = new UserCreatingPolicy(user, createUserDto);
        policy.CheckConstraints();

        return user;
    }
    
    public void Update(UpdateUserDto updateUserDto)
    {


        Email = updateUserDto.Email;
        FirstName = updateUserDto.FirstName;
        LastName = updateUserDto.LastName;
    }
}
