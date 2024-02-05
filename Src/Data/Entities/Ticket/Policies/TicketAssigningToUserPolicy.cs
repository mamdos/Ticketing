using Data.Common.Abstractions;
using Data.Common.Exceptions;
using Data.Entities.Ticket.Dtos;

namespace Data.Entities.Ticket.Policies;

internal class TicketAssigningToUserPolicy : BasePolicy<Aggregate.Ticket, AssignUserToTicketDto>
{
    public TicketAssigningToUserPolicy(in Aggregate.Ticket entity, in AssignUserToTicketDto input) : base(entity, input)
    {
    }

    internal override void CheckConstraints()
    {
        CheckIfUserAlreadyAssignedToTicket();
    }

    private void CheckIfUserAlreadyAssignedToTicket()
    {
        if (_entity.Assignee is not null && _entity.Assignee.Id == _input.Assignee.Id)
            throw new InvalidEntityStateException("cannot assign this ticket to a user that already assigned to the ticket");
    }
}
