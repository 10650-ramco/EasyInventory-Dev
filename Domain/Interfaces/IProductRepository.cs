using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        // =========================
        // RETRIEVAL
        // =========================
        Task<IEnumerable<Product>> GetAllAsync();

        // =========================
        // PAGINATION
        // =========================
        Task<PagedResult<Product>> GetPagedAsync(int page, int pageSize, string? searchTerm = null);

        // =========================
        // LOOKUPS
        // =========================
        Task<IEnumerable<Category>> GetCategoriesAsync();

        // =========================
        // CRUD
        // =========================
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
