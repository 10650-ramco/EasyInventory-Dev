using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Repository abstraction for Employee aggregate.
    /// Infrastructure layer will provide implementation.
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Gets all employees.
        /// </summary>
        Task<IReadOnlyList<Employee>> GetAllAsync(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets an employee by Id.
        /// </summary>
        Task<Employee?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new employee.
        /// UnitOfWork controls SaveChanges / transaction.
        /// </summary>
        Task AddAsync(
            Employee employee,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        Task UpdateAsync(
            Employee employee,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an employee by Id.
        /// </summary>
        Task DeleteAsync(
            int id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks whether an employee exists.
        /// Useful for validations.
        /// </summary>
        Task<bool> ExistsAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
