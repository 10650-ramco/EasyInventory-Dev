using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        // =========================
        // PAGINATION
        // =========================
        Task<PagedResult<Product>> GetPagedAsync(int page, int pageSize);

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
