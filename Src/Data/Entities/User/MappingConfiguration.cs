using AutoMapper;
using Data.Entities.User.Dtos;

namespace Data.Entities.User;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<Aggregate.User, UserDto>();
    }
}
