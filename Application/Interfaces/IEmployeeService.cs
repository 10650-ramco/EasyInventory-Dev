using Application.Common;
using Application.DTOs;

namespace Application.Interfaces
{
    /// <summary>
    /// Application service contract for Employee use-cases.
    /// Exposes DTOs to presentation layer.
    /// </summary>
    public interface IEmployeeService
    {
        // -------------------------
        // READ
        // -------------------------

        Task<IReadOnlyList<EmployeeDto>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<EmployeeDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        // -------------------------
        // WRITE
        // -------------------------

        Task<Result> CreateAsync(
            EmployeeDto dto,
            CancellationToken cancellationToken = default);

        Task<Result> UpdateAsync(
            EmployeeDto dto,
            CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
