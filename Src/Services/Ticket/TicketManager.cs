using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities.Ticket.Dtos;
using Data.Entities.User.Aggregate;
using Data.Persistence.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Common.Models;
using Services.Ticket.Dtos;

namespace Services.Ticket;

public interface ITicketManager
{
    public Task<ServiceResponse<IEnumerable<TicketDto>>> GetUserTicketsAsync(string IssuerId, CancellationToken cancellationToken);
    public Task<ServiceResponse<IEnumerable<TicketDto>>> GetTicketsAsync(CancellationToken cancellationToken);
    public Task<ServiceResponse> AddTicketAsync(CreateTicketRequestDto createTicketRequestDto, CancellationToken cancellationToken);
    public Task<ServiceResponse> UpdateTicketAsync(UpdateTicketRequestDto updateTicketRequestDto, CancellationToken cancellationToken);
    public Task<ServiceResponse> DeleteTicketAsync(DeleteTicketRequestDto deleteTicketRequestDto, CancellationToken cancellationToken);
    public Task<ServiceResponse> AssignUserAsync(AssignUserToTicketRequestDto assignUserToTicketRequestDto, CancellationToken cancellationToken);
    public Task<ServiceResponse> CloseTicketAsync(CloseTicketRequestDto closeTicketRequestDto, CancellationToken cancellationToken);
    public Task<ServiceResponse> InProgressTicketAsync(InProgressTicketRequestDto closeTicketRequestDto, CancellationToken cancellation);
}

public class TicketManager : ITicketManager
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public TicketManager(AppDbContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<ServiceResponse> AddTicketAsync(CreateTicketRequestDto createTicketRequestDto, CancellationToken cancellationToken)
    {
        var issuerUser = await _userManager.Users.FirstOrDefaultAsync(q => q.Id == createTicketRequestDto.IssuerId, cancellationToken);

        if (issuerUser is null)
            return ServiceResponse.Fail("issuer not found");

        var category = await _context.Categories.FirstOrDefaultAsync(q => q.Id == createTicketRequestDto.CategoryId, cancellationToken);

        if (category is null)
            return ServiceResponse.Fail("category not found");

        var createTicketDto = new CreateTicketDto(
            createTicketRequestDto.Title,
            createTicketRequestDto.Description,
            issuerUser,
            category);

        var createdTicket = Data.Entities.Ticket.Aggregate.Ticket.Create(createTicketDto);

        await _context.Tickets.AddAsync(createdTicket, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResponse.Successful();
    }

    public async Task<ServiceResponse> AssignUserAsync(AssignUserToTicketRequestDto assignUserToTicketRequestDto, CancellationToken cancellationToken)
    {
        var toUpdateTicket = await _context.Tickets
            .Include(q => q.Assignee)
            .FirstOrDefaultAsync(q => q.Id == assignUserToTicketRequestDto.Id, cancellationToken);

        if (toUpdateTicket is null)
            return ServiceResponse.Fail("ticket not found");

        var assigneeUser = await _userManager.Users.FirstOrDefaultAsync(q => q.Id == assignUserToTicketRequestDto.AssigneeUserId, cancellationToken);

        if (assigneeUser is null)
            return ServiceResponse.Fail("user not found");

        var assignUserToTicketDto = new AssignUserToTicketDto(assigneeUser);

        toUpdateTicket.AssignUser(assignUserToTicketDto);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResponse.Successful();
    }

    public async Task<ServiceResponse> CloseTicketAsync(CloseTicketRequestDto closeTicketRequestDto, CancellationToken cancellationToken)
    {
        var toCloseTicket = await _context.Tickets
            .FirstOrDefaultAsync(q => q.Id == closeTicketRequestDto.Id, cancellationToken);

        if (toCloseTicket is null)
            return ServiceResponse.Fail("ticket not found");

        toCloseTicket.ChangeStatusToClosed();

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResponse.Successful();
    }

    public async Task<ServiceResponse> DeleteTicketAsync(DeleteTicketRequestDto deleteTicketRequestDto, CancellationToken cancellationToken)
    {
        var toDeleteTicket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == deleteTicketRequestDto.Id, cancellationToken);

        if (toDeleteTicket is null)
            return ServiceResponse.Fail("ticket not found");

        _context.Tickets.Remove(toDeleteTicket);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResponse.Successful();
    }

    public async Task<ServiceResponse<IEnumerable<TicketDto>>> GetTicketsAsync(CancellationToken cancellationToken)
    {
        var result = await _context.Tickets
            .OrderBy(x => x.Id)
            .ProjectTo<TicketDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        return ServiceResponse<IEnumerable<TicketDto>>.Successful(result);
    }

    public async Task<ServiceResponse<IEnumerable<TicketDto>>> GetUserTicketsAsync(string IssuerId, CancellationToken cancellationToken)
    {
        var result = await _context.Tickets
            .Include(x => x.Issuer)
            .Where(x => x.Issuer.Id == IssuerId)
            .ProjectTo<TicketDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        return ServiceResponse<IEnumerable<TicketDto>>.Successful(result);
    }

    public async Task<ServiceResponse> InProgressTicketAsync(InProgressTicketRequestDto closeTicketRequestDto, CancellationToken cancellationToken)
    {
        var toUpdateTicket = await _context.Tickets
            .FirstOrDefaultAsync(q => q.Id == closeTicketRequestDto.Id, cancellationToken);

        if (toUpdateTicket is null)
            return ServiceResponse.Fail("ticket not found");

        toUpdateTicket.ChangeStatusToInProgress();

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResponse.Successful();
    }

    public async Task<ServiceResponse> UpdateTicketAsync(UpdateTicketRequestDto updateTicketRequestDto, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == updateTicketRequestDto.CategoryId, cancellationToken);

        if (category is null)
            return ServiceResponse.Fail("category not found");

        var toUpdateTicket = await _context.Tickets
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == updateTicketRequestDto.Id, cancellationToken);

        if (toUpdateTicket is null)
            return ServiceResponse.Fail("ticket not found");

        var updateTicketDto = new UpdateTicketDto(updateTicketRequestDto.Title, updateTicketRequestDto.Description, category);

        toUpdateTicket.Update(updateTicketDto);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResponse.Successful();
    }
}
