using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// EF Core implementation of User repository.
    /// </summary>
    public sealed class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // -------------------------
        // GET ALL
        // -------------------------
        public async Task<IReadOnlyList<User>> GetAllUserAsync(
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        // -------------------------
        // GET BY ID
        // -------------------------
        public async Task<User?> GetByUserIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        // -------------------------
        // GET BY USER NAME
        // ------------------------- 
        public async Task<User?> GetByUserNameAsync(
                string userName,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
        }

        // -------------------------
        // ADD
        // -------------------------
        public async Task AddUserAsync(
            User user,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
        }

        // -------------------------
        // UPDATE
        // -------------------------
        public Task UpdateUserAsync(
            User User,
            CancellationToken cancellationToken = default)
        {
            _dbContext.Users.Update(User);
            return Task.CompletedTask;
        }

        // -------------------------
        // DELETE
        // -------------------------
        public async Task DeleteUserAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var User = await _dbContext.Users
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            if (User != null)
            {
                _dbContext.Users.Remove(User);
            }
        }

        // -------------------------
        // EXISTS
        // -------------------------
        public async Task<bool> ExistsUserAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .AnyAsync(e => e.Id == id, cancellationToken);
        }
    }
}
