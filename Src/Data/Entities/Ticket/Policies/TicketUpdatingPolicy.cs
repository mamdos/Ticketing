using Data.Common.Abstractions;
using Data.Common.Exceptions;
using Data.Entities.Ticket.Dtos;

namespace Data.Entities.Ticket.Policies;

internal class TicketUpdatingPolicy : BasePolicy<Aggregate.Ticket, UpdateTicketDto>
{
    public TicketUpdatingPolicy(in Aggregate.Ticket entity, in UpdateTicketDto input) : base(entity, input)
    {
    }

    internal override void CheckConstraints()
    {
        CheckIfAllRequiredFieldsFilled();
    }

    private void CheckIfAllRequiredFieldsFilled()
    {
        if (string.IsNullOrWhiteSpace(_input.Title))
            throw new InvalidEntityStateException("all the required fields must be filled with data");
    }
}
