using Application.Common;
using Application.DTOs;

namespace Application.Interfaces
{
    /// <summary>
    /// Application service contract for Employee use-cases.
    /// Exposes DTOs to presentation layer.
    /// </summary>
    public interface IUserService
    {
        // -------------------------
        // READ
        // -------------------------

        Task<IReadOnlyList<UserDto>> GetAllUserAsync(
            CancellationToken cancellationToken = default);

        Task<UserDto?> GetUserByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<LoginResult> ValidateLoginAsync(
            LoginRequestDto request,
            CancellationToken cancellationToken = default);

        // -------------------------
        // WRITE
        // -------------------------

        Task<Result> CreateUserAsync(
            UserDto dto,
            CancellationToken cancellationToken = default);

        Task<Result> UpdateUserAsync(
            UserDto dto,
            CancellationToken cancellationToken = default);

        Task<Result> DeleteUserAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
