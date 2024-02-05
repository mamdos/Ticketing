using AutoMapper;
using Data.Entities.Ticket.Aggregate;
using Data.Entities.Ticket.Dtos;

namespace Data.Entities.Ticket;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<Aggregate.Ticket, TicketDto>();
    }
}
