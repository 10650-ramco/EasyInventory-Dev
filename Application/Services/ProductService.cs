using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;
using Domain.Common;
using Domain.Interfaces;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        var categories = await _repository.GetCategoriesAsync();

        return categories.Select(c => new CategoryDto
        {
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName
        });
    }

    public async Task AddAsync(ProductDto dto)
        => await _repository.AddAsync(dto.ToDomain());

    public async Task UpdateAsync(ProductDto dto)
        => await _repository.UpdateAsync(dto.ToDomain(dto.Id));

    public async Task DeleteAsync(int id)
        => await _repository.DeleteAsync(id);

    // =========================
    // PAGED PRODUCTS
    // =========================
    public async Task<PagedResult<ProductDto>> GetPagedAsync(int page, int pageSize)
    {
        if (page <= 0)
            throw new ArgumentOutOfRangeException(nameof(page));

        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize));

        var result = await _repository.GetPagedAsync(page, pageSize);

        var productDtos = result.Items
                                .Select(p => p.ToDto())
                                .ToList();

        return new PagedResult<ProductDto>(
            productDtos,
            result.TotalCount,
            page,
            pageSize);
    }
}
