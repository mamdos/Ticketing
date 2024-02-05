namespace Services.Ticket.Dtos;

public record CreateTicketRequestDto(string Title, string Description, string IssuerId, int CategoryId);