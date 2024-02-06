using Data.Common.Exceptions;
using Data.Entities.User.Aggregate;
using Data.Entities.User.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Persistence.Seed;

public static class UserSeeder
{
    public static async Task Initialize(IServiceProvider serviceProvider, string seedUserPassword)
    {
        var superUserId = await EnsureUser(serviceProvider, seedUserPassword);
        await EnsureRoles(
            serviceProvider,
            superUserId,
            UserRoles.GetAll().ToArray());
    }

    private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string seedUserPassword)
    {
        const string userName = "SuperUser";
        const string email = "SuperUser@test.com";

        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        var user = await userManager.FindByNameAsync(userName);
        if (user is null)
        {
            var createUserDto = new CreateUserDto(userName, email, userName, userName);
            user = User.Create(createUserDto);
            await userManager.CreateAsync(user, seedUserPassword);
        }

        if (user is null)
        {
            throw new InvalidEntityStateException("your password is weak");
        }

        return user.Id;
    }

    private static async Task EnsureRoles(IServiceProvider serviceProvider, string userId, string[] roles)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new Exception("super user not found");
        }

        foreach (var role in roles)
        {
            if (await roleManager.RoleExistsAsync(role) is false)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }


            await userManager.AddToRoleAsync(user, role);
        }
    }
}

