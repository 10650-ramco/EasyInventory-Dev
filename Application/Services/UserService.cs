using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;
using Domain.Interfaces;

namespace Application.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        // -------------------------
        // GET ALL
        // -------------------------
        public async Task<IReadOnlyList<UserDto>> GetAllUserAsync(
            CancellationToken cancellationToken = default)
        {
            var users = await _userRepository
                .GetAllUserAsync(cancellationToken);

            return users
                .Select(e => e.ToDto())
                .ToList();
        }

        // -------------------------
        // GET BY USER ID
        // -------------------------
        public async Task<UserDto?> GetUserByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository
                .GetByUserIdAsync(id, cancellationToken);

            return user?.ToDto();
        }

        // -------------------------
        // VALIDATE LOGIN
        // -------------------------
        public async Task<LoginResult> ValidateLoginAsync(
            LoginRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository
                .GetByUserNameAsync(request.UserName, cancellationToken);

            if (user == null)
                return LoginResult.Failure("User not found");

            // ⚠️ In production, NEVER store plain passwords
            if (user.Password != request.Password)
                return LoginResult.Failure("Invalid password");

            return LoginResult.Success();
        }

        // -------------------------
        // CREATE
        // -------------------------
        public async Task<Result> CreateUserAsync(
            UserDto dto,
            CancellationToken cancellationToken = default)
        {
            var user = dto.ToDomain();

            await _userRepository.AddUserAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        //// -------------------------
        //// UPDATE
        //// -------------------------
        public async Task<Result> UpdateUserAsync(
            UserDto dto,
            CancellationToken cancellationToken = default)
        {
            if (!await _userRepository.ExistsUserAsync(dto.Id, cancellationToken))
            {
                return Result.Failure("Employee not found");
            }

            var user = dto.ToDomain(dto.Id);

            await _userRepository.UpdateUserAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        //// -------------------------
        //// DELETE
        //// -------------------------
        public async Task<Result> DeleteUserAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            if (!await _userRepository.ExistsUserAsync(id, cancellationToken))
            {
                return Result.Failure("Employee not found");
            }

            await _userRepository.DeleteUserAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
