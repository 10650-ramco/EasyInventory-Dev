using Application.DTOs;
using Domain.Common;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<PagedResult<CategoryDto>> GetPagedAsync(int page, int pageSize, string? searchTerm = null);
        Task<bool> IsNameUniqueAsync(string name, int? excludeId = null);
        Task AddAsync(CategoryDto dto);
        Task UpdateAsync(CategoryDto dto);
        Task DeleteAsync(int id);
    }
}
