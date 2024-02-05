using Data.Common.Abstractions;

namespace Data.Entities.Ticket.Dtos;

public record UpdateTicketDto(
    string Title,
    string? Description,
    Category.Aggregate.Category Category) : IDto;