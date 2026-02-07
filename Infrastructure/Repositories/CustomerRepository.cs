using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                                 .AsNoTracking()
                                 .OrderBy(c => c.CustomerName)
                                 .ToListAsync();
        }

        public async Task<PagedResult<Customer>> GetPagedAsync(int page, int pageSize, string? searchTerm)
        {
            var query = _context.Customers.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(c => c.CustomerName.ToLower().Contains(searchTerm) || 
                                       c.CustomerCode.ToLower().Contains(searchTerm) ||
                                       (c.PhoneNumber != null && c.PhoneNumber.Contains(searchTerm)) ||
                                       (c.Email != null && c.Email.ToLower().Contains(searchTerm)));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(c => c.CustomerId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Customer>(items, totalCount, page, pageSize);
        }


        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            var trackedEntity = _context.Customers.Local.FirstOrDefault(e => e.CustomerId == customer.CustomerId);
            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsCodeUniqueAsync(string customerCode, int? excludeId = null)
        {
            var query = _context.Customers.AsQueryable();
            if (excludeId.HasValue)
                query = query.Where(c => c.CustomerId != excludeId.Value);

            return !await query.AnyAsync(c => c.CustomerCode.ToLower() == customerCode.ToLower());
        }
    }
}
