using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Repository abstraction for Employee aggregate.
    /// Infrastructure layer will provide implementation.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets all employees.
        /// </summary>
        Task<IReadOnlyList<User>> GetAllUserAsync(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets an employee by Id.
        /// </summary>
        Task<User?> GetByUserIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets an login user by user name.
        /// </summary>
        Task<User?> GetByUserNameAsync(
            string userName,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new employee.
        /// UnitOfWork controls SaveChanges / transaction.
        /// </summary>
        Task AddUserAsync(
            User user,
            CancellationToken cancellationToken = default);

        ///// <summary>
        ///// Updates an existing employee.
        ///// </summary>
        Task UpdateUserAsync(
            User user,
            CancellationToken cancellationToken = default);

        ///// <summary>
        ///// Deletes an employee by Id.
        ///// </summary>
        Task DeleteUserAsync(
            int id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks whether an employee exists.
        /// Useful for validations.
        /// </summary>
        Task<bool> ExistsUserAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
