using Application.DTOs;
using Domain.Entities;

namespace Application.Mapping
{
    /// <summary>
    /// Maps User between Domain and DTO.
    /// Mapping belongs to Application layer by design.
    /// </summary>
    public static class UserMapper
    {
        // -------------------------
        // Domain → DTO
        // -------------------------
        public static UserDto ToDto(this User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                LastName = user.LastName,
                Password = user.Password,
                Email = user.Email
            };
        }

        // -------------------------
        // DTO → Domain (CREATE)
        // -------------------------
        public static User ToDomain(this UserDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            return User.Create(
                userName: dto.UserName.Trim(),
                name: dto.Name.Trim(),
                email: dto.Email.Trim(),
                password: dto.Password.Trim(),
                lastName: dto.LastName.Trim());
        }

        // -------------------------
        // DTO → Domain (UPDATE)
        // -------------------------
        public static User ToDomain(this UserDto dto, int id)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            return User.Update(
                id: id,
                userName: dto.UserName.Trim(),
                name: dto.Name.Trim(),
                email: dto.Email.Trim(),
                password: dto.Password.Trim(),
                lastName: dto.LastName.Trim());
        }
    }
}
