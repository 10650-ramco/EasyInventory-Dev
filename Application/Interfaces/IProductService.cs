using Application.DTOs;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IProductService
    {
        // =========================
        // RETRIEVAL
        // =========================
        Task<IEnumerable<ProductDto>> GetAllAsync();

        // =========================
        // PAGINATION
        // =========================
        Task<PagedResult<ProductDto>> GetPagedAsync(int page, int pageSize, string? searchTerm = null);

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
