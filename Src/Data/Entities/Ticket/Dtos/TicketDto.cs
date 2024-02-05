using Data.Common.Abstractions;
using Data.Entities.Category.Dtos;
using Data.Entities.User.Dtos;

namespace Data.Entities.Ticket.Dtos;

public record TicketDto(
    long Id,
    string Title,
    string? Description,
    UserDto Issuer,
    UserDto? Assignee,
    CategoryDto Category) : IDto;
