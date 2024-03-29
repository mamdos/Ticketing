﻿using Data.Common.Abstractions;

namespace Data.Entities.Ticket.Dtos;

public record CreateTicketDto(
    string Title,
    string? Description,
    User.Aggregate.User Issuer,
    Category.Aggregate.Category Category) : IDto;