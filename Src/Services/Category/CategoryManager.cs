using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities.Category.Aggregate;
using Data.Entities.Category.Dtos;
using Data.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Services.Category.Dto;
using Services.Common.Models;

namespace Services.Category;

public interface ICategoryManager
{
    public Task<ServiceResponse> CreateAsync(CreateCategoryRequestDto createCategoryRequestDto, CancellationToken cancellationToken);
    public Task<ServiceResponse> UpdateAsync(UpdateCategoryRequestDto updateCategoryRequestDto, CancellationToken cancellationToken);
    public Task<ServiceResponse> DeleteAsync(DeleteCategoryReqeustDto deleteCategoryReqeustDto, CancellationToken cancellationToken);
    public Task<ServiceResponse<IEnumerable<CategoryDto>>> GetCategoriesAsync(CancellationToken cancellationToken);
}

public class CategoryManager : ICategoryManager
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CategoryManager(AppDbContext appDbContext, IMapper mapper)
    {
        _context = appDbContext;
        _mapper = mapper;
    }

    public async Task<ServiceResponse> CreateAsync(CreateCategoryRequestDto createCategoryRequestDto, CancellationToken cancellationToken)
    {
        var createCategoryDto = new CreateCategoryDto(createCategoryRequestDto.Name);

        var createdCategory = Data.Entities.Category.Aggregate.Category.Create(createCategoryDto);

        await _context.Categories.AddAsync(createdCategory, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResponse.Successful();
    }

    public async Task<ServiceResponse> DeleteAsync(DeleteCategoryReqeustDto deleteCategoryReqeustDto, CancellationToken cancellationToken)
    {
        var toDeleteCategory = await _context.Categories.FirstOrDefaultAsync(q => q.Id == deleteCategoryReqeustDto.Id, cancellationToken);

        if (toDeleteCategory is null)
            return ServiceResponse.Fail("category not found");

        _context.Categories.Remove(toDeleteCategory);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResponse.Successful();
    }

    public async Task<ServiceResponse<IEnumerable<CategoryDto>>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var result = await _context.Categories
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        return ServiceResponse<IEnumerable<CategoryDto>>.Successful(result);
    }

    public async Task<ServiceResponse> UpdateAsync(UpdateCategoryRequestDto updateCategoryRequestDto, CancellationToken cancellationToken)
    {
        var toUpdateCategory = await _context.Categories
            .FirstOrDefaultAsync(q => q.Id == updateCategoryRequestDto.Id, cancellationToken);

        if (toUpdateCategory is null)
            return ServiceResponse.Fail("category not found");

        var updateCategoryDto = new UpdateCategoryDto(updateCategoryRequestDto.Name);

        toUpdateCategory.Update(updateCategoryDto);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResponse.Successful();
    }
}
