using Data.Common.Abstractions;

namespace Data.Entities.Ticket.Dtos;

public record CreateTicketDto(
    string Title,
    string IssuerUserId,
    Category.Aggregate.Category Category) : IDto;