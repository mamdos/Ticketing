using Data.Common.Abstractions;
using Data.Common.Exceptions;

namespace Data.Entities.Ticket.Policies;

internal sealed class TicketChangingStatusToClosedPolicy : BasePolicy<Aggregate.Ticket>
{
    public TicketChangingStatusToClosedPolicy(Aggregate.Ticket entity) : base(entity)
    {
    }

    internal override void CheckConstraints()
    {
        CheckIfStatusIsNotClosed();
    }

    private void CheckIfStatusIsNotClosed()
    {
        if (_entity.Status == Aggregate.TicketStatus.Closed)
            throw new InvalidEntityStateException("ticket status cannot be changed to in progress while it's in progress already");
    }
}
