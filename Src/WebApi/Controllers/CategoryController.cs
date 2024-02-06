using Data.Entities.User.Aggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Category;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Authorize(Roles = $"{UserRoles.CategoryAdmin},{UserRoles.TicketUser}")]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryManager _categoryManager;

    public CategoryController(ICategoryManager categoryManager)
    {
        _categoryManager = categoryManager;
    }

    [HttpGet("Categories")]
    public async Task<IActionResult> Categories()
    {
        var result = await _categoryManager.GetCategoriesAsync(HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful(result.Data));

        return BadRequest(ApiResponse.Fail(result.Message));
    }
}
