using Data.Common.Abstractions;

namespace Data.Entities.Ticket.Dtos;

public record AssignUserToTicketDto(User.Aggregate.User Assignee) : IDto;