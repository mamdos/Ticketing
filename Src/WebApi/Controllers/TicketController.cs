using Data.Entities.User.Aggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize(Roles = UserRoles.IssuerUser)]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    [HttpGet("MyTickets")]
    public async Task<IActionResult> MyTickets()
    {
        throw new NotImplementedException();
    }

    [HttpPost("NewTicket")]
    public async Task<IActionResult> New()
    {
        throw new NotImplementedException();
    }

    [HttpPut("EditTicket")]
    public async Task<IActionResult> Edit()
    {
        throw new NotImplementedException();
    }

    [HttpDelete("DeleteTicket")]
    public async Task<IActionResult> Delete()
    {
        throw new NotImplementedException();
    }
}
