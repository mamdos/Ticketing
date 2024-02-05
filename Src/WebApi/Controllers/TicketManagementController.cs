using Data.Entities.User.Aggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Ticket;
using Services.Ticket.Dtos;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Authorize(Roles = UserRoles.SupportUser)]
[Route("api/Admin/[controller]")]
public class TicketManagementController : ControllerBase
{
    private readonly ITicketManager _ticketManager;

    public TicketManagementController(ITicketManager ticketManager)
    {
        _ticketManager = ticketManager;
    }

    [HttpGet("Tickets")]
    public async Task<IActionResult> Tickets()
    {
        var result = await _ticketManager.GetTicketsAsync(HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful(result.Data));

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }

    [HttpPatch("CloseTicket")]
    public async Task<IActionResult> Close([FromBody] CloseTicketRequestDto closeTicketRequestDto)
    {
        var result = await _ticketManager.CloseTicketAsync(closeTicketRequestDto, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful());

        return BadRequest(ApiResponse.Fail(message: result.Message));

    }

    [HttpPatch("InProgressTicket")]
    public async Task<IActionResult> InProgress([FromBody] InProgressTicketRequestDto inProgressTicketRequestDto)
    {
        var result = await _ticketManager.InProgressTicketAsync(inProgressTicketRequestDto, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful());

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }

    [HttpPatch("AssignTicketToUser")]
    public async Task<IActionResult> AssignToUser([FromBody] AssignUserToTicketRequestDto assignUserToTicketRequestDto)
    {
        var result = await _ticketManager.AssignUserAsync(assignUserToTicketRequestDto, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful());

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }
}
