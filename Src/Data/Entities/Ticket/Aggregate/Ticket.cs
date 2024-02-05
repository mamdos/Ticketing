using Data.Common.Abstractions;
using Data.Entities.Ticket.Dtos;
using Data.Entities.Ticket.Policies;

namespace Data.Entities.Ticket.Aggregate;

public class Ticket : BaseEntity<long>, IAggregateRoot
{
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public TicketStatus Status { get; private set; }
    public User.Aggregate.User Issuer { get; set; } = null!;
    public User.Aggregate.User? Assignee { get; private set; }
    public Category.Aggregate.Category Category { get; private set; } = null!;

    protected Ticket()
    { }

    public static Ticket Create(CreateTicketDto createTicketDto)
    {

        Ticket createdTicket = new()
        {
            Title = createTicketDto.Title,
            Description = createTicketDto.Description,
            Issuer = createTicketDto.Issuer,
            Category = createTicketDto.Category,
        };

        var policy = new TicketCreatingPolicy(createdTicket, createTicketDto);
        policy.CheckConstraints();

        return createdTicket;
    }

    public void Update(UpdateTicketDto updateTicketDto)
    {
        var policy = new TicketUpdatingPolicy(this, updateTicketDto);
        policy.CheckConstraints();

        Title = updateTicketDto.Title;
        Description = updateTicketDto.Description;
        Category = updateTicketDto.Category;
    }

    public void ChangeStatusToInProgress()
    {
        var policy = new TicketChangingStatusToInProgressPolicy(this);
        policy.CheckConstraints();

        Status = TicketStatus.InProgress;
    }

    public void ChangeStatusToClosed()
    {
        var policy = new TicketChangingStatusToClosedPolicy(this);
        policy.CheckConstraints();

        Status = TicketStatus.Closed;
    }

    public void AssignUser(AssignUserToTicketDto assignUserToTicketDto)
    {
        var policy = new TicketAssigningToUserPolicy(this, assignUserToTicketDto);
        policy.CheckConstraints();

        Assignee = assignUserToTicketDto.Assignee;
    }
}
