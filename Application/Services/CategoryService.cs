using Application.DTOs;
using Application.Interfaces;
using Application.Mapping;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => c.ToDto());
        }

        public async Task<PagedResult<CategoryDto>> GetPagedAsync(int page, int pageSize, string? searchTerm = null)
        {
            var result = await _repository.GetPagedAsync(page, pageSize, searchTerm);
            var dtos = result.Items.Select(c => c.ToDto()).ToList();
            
            return new PagedResult<CategoryDto>(dtos, result.TotalCount, page, pageSize);
        }

        public async Task<bool> IsNameUniqueAsync(string name, int? excludeId = null)
        {
            return !await _repository.ExistsByNameAsync(name, excludeId);
        }

        public async Task AddAsync(CategoryDto dto)
        {
            await _repository.AddAsync(dto.ToDomain());
        }

        public async Task UpdateAsync(CategoryDto dto)
        {
            await _repository.UpdateAsync(dto.ToDomain());
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
