    using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    /// <summary>
    /// Maps Employee between Domain and DTO.
    /// Mapping belongs to Application layer by design.
    /// </summary>
    public static class EmployeeMapper
    {
        // -------------------------
        // Domain → DTO
        // -------------------------
        public static EmployeeDto ToDto(this Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department
            };
        }

        // -------------------------
        // DTO → Domain (CREATE)
        // -------------------------
        public static Employee ToDomain(this EmployeeDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            return Employee.Create(
                name: dto.Name.Trim(),
                department: dto.Department.Trim());
        }

        // -------------------------
        // DTO → Domain (UPDATE)
        // -------------------------
        public static Employee ToDomain(this EmployeeDto dto, int id)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            return Employee.Update(
                id: id,
                name: dto.Name.Trim(),
                department: dto.Department.Trim());
        }
    }
}
