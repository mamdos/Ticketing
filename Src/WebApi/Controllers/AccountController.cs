using Microsoft.AspNetCore.Mvc;
using Services.User;
using Services.User.Dtos;
using System.Diagnostics.Contracts;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ISignInManager _signInManager;

    public AccountController(ISignInManager signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequestDto signInRequestDto)
    {
        var result = await _signInManager.SignInAsync(signInRequestDto, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful(result.Data));

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto signUpRequestDto)
    {
        var result = await _signInManager.SignUpAsync(signUpRequestDto, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful(result.Data));

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }
}
