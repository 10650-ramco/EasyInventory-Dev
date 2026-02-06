using Application.DTOs;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IProductService
    {
        // =========================
        // PAGINATION
        // =========================
        Task<PagedResult<ProductDto>> GetPagedAsync(int page, int pageSize);

        // =========================
        // LOOKUPS
        // =========================
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

        // =========================
        // CRUD
        // =========================
        Task AddAsync(ProductDto dto);
        Task UpdateAsync(ProductDto dto);
        Task DeleteAsync(int id);
    }
}
