using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<PagedResult<Customer>> GetPagedAsync(int page, int pageSize, string? searchTerm);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
        Task<bool> IsCodeUniqueAsync(string customerCode, int? excludeId = null);
    }
}
