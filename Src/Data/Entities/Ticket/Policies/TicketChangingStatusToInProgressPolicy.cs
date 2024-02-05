using Data.Common.Abstractions;
using Data.Common.Exceptions;

namespace Data.Entities.Ticket.Policies;

internal sealed class TicketChangingStatusToInProgressPolicy : BasePolicy<Aggregate.Ticket>
{
    internal TicketChangingStatusToInProgressPolicy(Aggregate.Ticket entity) : base(entity)
    {
    }

    internal override void CheckConstraints()
    {
        CheckStatusIsNotInProgressAlready();
    }

    private void CheckStatusIsNotInProgressAlready()
    {
        if (_entity.Status == Aggregate.TicketStatus.InProgress)
            throw new InvalidEntityStateException("ticket status cannot be changed to in progress while it's in progress already");
    }
}
