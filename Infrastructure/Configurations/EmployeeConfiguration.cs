using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Table name
            builder.ToTable("Employees");

            // Primary key
            builder.HasKey(e => e.Id);

            // Identity column
            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            // Columns
            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Department)
                   .IsRequired()
                   .HasMaxLength(50);

            // Optional: Index
            builder.HasIndex(e => e.Name);
        }
    }
}
