using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities.User.Aggregate;
using Data.Entities.User.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Common.Models;
using Services.User.Dtos;

namespace Services.User;



public interface IUserManager
{
    public Task<ServiceResponse> CreateAsync(CreateUserRequestDto createUserRequestDto, CancellationToken cancellationToken);
    public Task<ServiceResponse> UpdateAsync(UpdateUserRequestDto updateUserRequestDto, CancellationToken cancellationToken);
    public Task<ServiceResponse> DeleteAsync(DeleteUserRequestDto deleteUserRequestDto, CancellationToken cancellationToken);
    public Task<ServiceResponse<IEnumerable<UserDto>>> GetUsers(CancellationToken cancellationToken);
}


public class UserManager : IUserManager
{
    private readonly UserManager<Data.Entities.User.Aggregate.User> _identityUserManager;
    private readonly IMapper _mapper;

    public UserManager(UserManager<Data.Entities.User.Aggregate.User> identityUserManager, IMapper mapper)
    {
        _identityUserManager = identityUserManager;
        _mapper = mapper;
    }

    public async Task<ServiceResponse> CreateAsync(CreateUserRequestDto createUserRequestDto, CancellationToken cancellationToken)
    {
        var createUserDto = new CreateUserDto(
            createUserRequestDto.UserName,
            createUserRequestDto.Email,
            createUserRequestDto.FirstName,
            createUserRequestDto.LastName);

        var toAddUser = Data.Entities.User.Aggregate.User.Create(createUserDto);

        var creationResult = await _identityUserManager.CreateAsync(toAddUser);

        if (creationResult.Succeeded is false)
            return ServiceResponse
                .Fail(string.Join(',', creationResult.Errors.Select(x => x.Description)) ?? "something went wrong");

        var roleAdditionResult = await _identityUserManager.AddToRolesAsync(toAddUser, createUserRequestDto.Roles);

        if (roleAdditionResult.Succeeded is false)
            return ServiceResponse
                .Fail(string.Join(',', roleAdditionResult.Errors.Select(x => x.Description)) ?? "something went wrong");


        var passwordAdditionResult = await _identityUserManager.AddPasswordAsync(toAddUser, createUserRequestDto.Password);

        if (passwordAdditionResult.Succeeded is false)
            return ServiceResponse
                .Fail(string.Join(',', passwordAdditionResult.Errors.Select(x => x.Description)) ?? "something went wrong");


        return ServiceResponse.Successful();
    }

    public async Task<ServiceResponse> DeleteAsync(DeleteUserRequestDto deleteUserRequestDto, CancellationToken cancellationToken)
    {
        var toDeleteUser = await _identityUserManager.Users
            .FirstOrDefaultAsync(q => q.Id == deleteUserRequestDto.Id, cancellationToken);

        if (toDeleteUser is null)
            return ServiceResponse.Fail("user not found");

        var deletionResult = await _identityUserManager.DeleteAsync(toDeleteUser);

        if (deletionResult.Succeeded is false)
            return ServiceResponse
                .Fail(string.Join(',', deletionResult.Errors.Select(x => x.Description)) ?? "something went wrong");

        return ServiceResponse.Successful();
    }

    public async Task<ServiceResponse<IEnumerable<UserDto>>> GetUsers(CancellationToken cancellationToken)
    {
        var result = await _identityUserManager.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        return ServiceResponse<IEnumerable<UserDto>>.Successful(result);
    }

    public async Task<ServiceResponse> UpdateAsync(UpdateUserRequestDto updateUserRequestDto, CancellationToken cancellationToken)
    {
        var toUpdateUser = await _identityUserManager.Users
            .FirstOrDefaultAsync(q => q.Id == updateUserRequestDto.Id, cancellationToken);

        if (toUpdateUser is null)
            return ServiceResponse.Fail("user not found");

        var updateUserDto = new UpdateUserDto(updateUserRequestDto.Email, updateUserRequestDto.FirstName, updateUserRequestDto.LastName);

        toUpdateUser.Update(updateUserDto);

        var updateResult = await _identityUserManager.UpdateAsync(toUpdateUser);

        if (updateResult.Succeeded is false)
            return ServiceResponse
                .Fail(string.Join(',', updateResult.Errors.Select(x => x.Description)) ?? "something went wrong");

        await _identityUserManager.RemoveFromRolesAsync(toUpdateUser, UserRoles.GetAll().ToArray());
        await _identityUserManager.AddToRolesAsync(toUpdateUser, UserRoles.GetAll().ToArray());

        return ServiceResponse.Successful();
    }
}
