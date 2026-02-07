
using Application.DTOs;
using Domain.Common;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<PagedResult<CustomerDto>> GetPagedAsync(int page, int pageSize, string? searchTerm = null);
        Task<CustomerDto?> GetByIdAsync(int id);
        Task AddAsync(CustomerDto dto);
        Task UpdateAsync(CustomerDto dto);
        Task DeleteAsync(int id);
        Task<bool> IsCodeUniqueAsync(string customerCode, int? excludeId = null);
    }
}
