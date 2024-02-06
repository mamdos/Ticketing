using Data.Entities.User.Aggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Common.Models;
using Services.User.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;

namespace Services.User;

public interface ISignInManager
{
    public Task<ServiceResponse<string>> SignUpAsync(SignUpRequestDto signUpRequestDto, CancellationToken cancellationToken);
    public Task<ServiceResponse<string>> SignInAsync(SignInRequestDto signInRequestDto, CancellationToken cancellationToken);
}

public class SignInManager : ISignInManager
{
    private readonly UserManager<Data.Entities.User.Aggregate.User> _identityUserManager;
    private readonly IUserManager _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public SignInManager(UserManager<Data.Entities.User.Aggregate.User> identityUserManager, IUserManager userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _identityUserManager = identityUserManager;
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ServiceResponse<string>> SignInAsync(SignInRequestDto signInRequestDto, CancellationToken cancellationToken)
    {
        var toSignInUser = await _identityUserManager.Users
            .FirstOrDefaultAsync(q => q.UserName == signInRequestDto.UserName, cancellationToken);

        if (toSignInUser is null)
            return ServiceResponse<string>.Fail("username or password is incorrect");

        var isPasswordCorrect = await _identityUserManager.CheckPasswordAsync(toSignInUser, signInRequestDto.Password);

        if (isPasswordCorrect)
        {
            var user = await _identityUserManager.Users.FirstAsync(q => q.Id == toSignInUser.Id, cancellationToken);

            string token = await GenerateToken(user);

            return ServiceResponse<string>.Successful(token);
        }

        return ServiceResponse<string>.Fail("username or password is incorrect");
    }

    public async Task<ServiceResponse<string>> SignUpAsync(SignUpRequestDto signUpRequestDto, CancellationToken cancellationToken)
    {
        var createUserRequestDto = new CreateUserRequestDto(
            signUpRequestDto.UserName,
            signUpRequestDto.Email,
            signUpRequestDto.FirstName,
            signUpRequestDto.LastName,
            signUpRequestDto.Password,
            [UserRoles.TicketUser]);

        var creationResult = await _userManager.CreateAsync(createUserRequestDto, cancellationToken);

        if (creationResult.IsSucceed)
        {
            var user = await _identityUserManager.Users.FirstAsync(q => q.UserName == signUpRequestDto.UserName, cancellationToken);

            var token = await GenerateToken(user);

            return ServiceResponse<string>.Successful(token);
        }

        return ServiceResponse<string>.Fail(creationResult.Message ?? "something went wrong");
    }

    private async Task<string> GenerateToken(Data.Entities.User.Aggregate.User user)
    {
        var userClaims = await _identityUserManager.GetClaimsAsync(user);
        var userRoles = await _identityUserManager.GetRolesAsync(user);

        userClaims.Add(new Claim(ClaimTypes.Name, user.UserName!));
        userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        foreach (var role in userRoles)
        {
            userClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = _jwtTokenGenerator.GenerateToken(userClaims);
        return token;
    }
}
