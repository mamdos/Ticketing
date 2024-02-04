using Data.Common.Abstractions;
using Data.Entities.Ticket.Dtos;
using Data.Entities.Ticket.Policies;

namespace Data.Entities.Ticket.Aggregate;

public class Ticket : BaseEntity<long>, IAggregateRoot
{
    public string Title { get; private set; } = null!;
    public TicketStatus Status { get; private set; }
    public string IssuerUserId { get; private set; } = null!;
    public string? AssigneeUserId { get; private set; }
    public Category.Aggregate.Category Category { get; private set; } = null!;

    protected Ticket()
    { }

    public static Ticket Create(CreateTicketDto createTicketDto)
    {

        Ticket createdTicket = new()
        {
            Title = createTicketDto.Title,
            IssuerUserId = createTicketDto.IssuerUserId,
            Category = createTicketDto.Category,
        };

        var policy = new TicketCreatingPolicy(createdTicket, createTicketDto);
        policy.CheckConstraints();

        return createdTicket;
    }
}
