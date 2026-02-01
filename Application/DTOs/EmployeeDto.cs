namespace Application.DTOs
{
    public sealed class EmployeeDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Department { get; init; } = string.Empty;
    }
}