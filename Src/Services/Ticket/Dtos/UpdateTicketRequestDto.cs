namespace Services.Ticket.Dtos;

public record UpdateTicketRequestDto(long Id, string Title, string? Description, int CategoryId);