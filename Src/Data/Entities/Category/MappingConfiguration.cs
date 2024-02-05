using AutoMapper;
using Data.Entities.Category.Dtos;

namespace Data.Entities.Category;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<Aggregate.Category, CategoryDto>();
    }
}
