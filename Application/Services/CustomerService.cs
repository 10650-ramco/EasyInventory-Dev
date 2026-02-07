
using Application.DTOs;
using Application.Interfaces;
using Application.Mapping;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _repository.GetAllAsync();
            return customers.Select(c => c.ToDto());
        }

        public async Task<PagedResult<CustomerDto>> GetPagedAsync(int page, int pageSize, string? searchTerm = null)
        {
            var result = await _repository.GetPagedAsync(page, pageSize, searchTerm);

            var dtos = result.Items
                             .Select(c => c.ToDto())
                             .ToList();

            return new PagedResult<CustomerDto>(
                dtos,
                result.TotalCount,
                page,
                pageSize);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            return customer?.ToDto();
        }

        public async Task AddAsync(CustomerDto dto)
        {
            await _repository.AddAsync(dto.ToDomain());
        }

        public async Task UpdateAsync(CustomerDto dto)
        {
            await _repository.UpdateAsync(dto.ToDomain(dto.CustomerId));
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> IsCodeUniqueAsync(string customerCode, int? excludeId = null)
        {
            return await _repository.IsCodeUniqueAsync(customerCode, excludeId);
        }
    }
}
