using Data.Entities.User.Aggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Ticket;
using Services.Ticket.Dtos;
using System.Security.Claims;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Authorize(Roles = UserRoles.IssuerUser)]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketManager _ticketManager;

    public TicketController(ITicketManager ticketManager)
    {
        _ticketManager = ticketManager;
    }

    [HttpGet("MyTickets")]
    public async Task<IActionResult> MyTickets()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

        var result = await _ticketManager.GetUserTicketsAsync(userId, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful(result.Data));

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }

    [HttpPost("NewTicket")]
    public async Task<IActionResult> New([FromBody] CreateTicketRequestDto createTicketRequestDto)
    {
        var result = await _ticketManager.AddTicketAsync(createTicketRequestDto, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful());

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }

    [HttpPut("EditTicket")]
    public async Task<IActionResult> Edit([FromBody] UpdateTicketRequestDto updateTicketRequestDto)
    {
        var result = await _ticketManager.UpdateTicketAsync(updateTicketRequestDto, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful());

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }

    [HttpDelete("DeleteTicket")]
    public async Task<IActionResult> Delete([FromBody] DeleteTicketRequestDto deleteTicketRequestDto)
    {
        var result = await _ticketManager.DeleteTicketAsync(deleteTicketRequestDto, HttpContext.RequestAborted);

        if (result.IsSucceed)
            return Ok(ApiResponse.Successful());

        return BadRequest(ApiResponse.Fail(message: result.Message));
    }
}
