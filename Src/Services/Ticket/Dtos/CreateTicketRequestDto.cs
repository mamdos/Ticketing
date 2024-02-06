namespace Services.Ticket.Dtos;

public record CreateTicketRequestDto(string Title, string Description, int CategoryId)
{
    public string IssuerId { get; set; } = "";
}