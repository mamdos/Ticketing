using Data.Entities.User.Aggregate;
using Data.Persistence.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.User;
using Services.User.Dtos;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Authorize(Roles = UserRoles.UserAdmin)]
[Route("api/Admin/[controller]")]
public class UserManagmentController : ControllerBase
{
    private readonly IUserManager _userManager;

    public UserManagmentController(IUserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("Users")]
    [Authorize(Roles = $"{UserRoles.UserAdmin},{UserRoles.TicketAdmin}")]
    public async Task<IActionResult> Users()
    {
        var result = await _userManager.GetUsers(HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful(result.Data));

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }

    [HttpPost("CreateUser")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequestDto createUserRequestDto)
    {
        var result = await _userManager.CreateAsync(createUserRequestDto, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful());

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }

    [HttpPut("EditUser")]
    public async Task<IActionResult> Edit([FromBody] UpdateUserRequestDto updateUserRequestDto)
    {
        var result = await _userManager.UpdateAsync(updateUserRequestDto, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful());

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }

    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> Delete([FromBody] DeleteUserRequestDto deleteUserRequestDto)
    {
        var result = await _userManager.DeleteAsync(deleteUserRequestDto, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful());

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }
}
