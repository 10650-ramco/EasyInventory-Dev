using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// EF Core implementation of Employee repository.
    /// </summary>
    public sealed class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // -------------------------
        // GET ALL
        // -------------------------
        public async Task<IReadOnlyList<Employee>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Employees
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        // -------------------------
        // GET BY ID
        // -------------------------
        public async Task<Employee?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        // -------------------------
        // ADD
        // -------------------------
        public async Task AddAsync(
            Employee employee,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Employees.AddAsync(employee, cancellationToken);
        }

        // -------------------------
        // UPDATE
        // -------------------------
        public Task UpdateAsync(
            Employee employee,
            CancellationToken cancellationToken = default)
        {
            _dbContext.Employees.Update(employee);
            return Task.CompletedTask;
        }

        // -------------------------
        // DELETE
        // -------------------------
        public async Task DeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var employee = await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
            }
        }

        // -------------------------
        // EXISTS
        // -------------------------
        public async Task<bool> ExistsAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Employees
                .AnyAsync(e => e.Id == id, cancellationToken);
        }
    }
}
