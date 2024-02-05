using Data.Common.Abstractions;
using Data.Common.Exceptions;
using Data.Entities.Ticket.Dtos;

namespace Data.Entities.Ticket.Policies;

internal sealed class TicketCreatingPolicy : BasePolicy<Aggregate.Ticket, CreateTicketDto>
{
    public TicketCreatingPolicy(in Aggregate.Ticket entity,in CreateTicketDto input) : base(entity, input)
    {
    }

    internal override void CheckConstraints()
    {
        CheckInputsAreFilled();
    }

    private void CheckInputsAreFilled()
    {
        if (string.IsNullOrWhiteSpace(_input.Title))
            throw new InvalidEntityStateException("all the required input fileds must be filled with data");
    }
}
