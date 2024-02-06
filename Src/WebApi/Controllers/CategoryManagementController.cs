using Data.Entities.User.Aggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Category;
using Services.Category.Dto;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Authorize(Roles = UserRoles.CategoryAdmin)]
[Route("api/Admin/[controller]")]
public class CategoryManagementController : ControllerBase
{
    private readonly ICategoryManager _categoryManager;

    public CategoryManagementController(ICategoryManager categoryManager)
    {
        _categoryManager = categoryManager;
    }

    [HttpPost("AddCategory")]
    public async Task<IActionResult> Add([FromBody] CreateCategoryRequestDto createCategoryRequestDto)
    {
        var result = await _categoryManager.CreateAsync(createCategoryRequestDto, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful());

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }

    [HttpPut("EditCategory")]
    public async Task<IActionResult> Edit([FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
    {
        var result = await _categoryManager.UpdateAsync(updateCategoryRequestDto, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful());

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }

    [HttpDelete("DeleteCategory")]
    public async Task<IActionResult> Delete([FromBody] DeleteCategoryReqeustDto deleteCategoryReqeustDto)
    {
        var result = await _categoryManager.DeleteAsync(deleteCategoryReqeustDto, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful());

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }
}
