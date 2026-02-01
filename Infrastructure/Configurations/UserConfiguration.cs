using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table name
            builder.ToTable("Users");

            // Primary key
            builder.HasKey(e => e.Id);

            // Identity column
            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            // Columns
            builder.Property(e => e.UserName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Password)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(50);


            builder.Property(e => e.LastName)
                   //.IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.Email)
                   //.IsRequired()
                   .HasMaxLength(50);

            // Optional: Index
            builder.HasIndex(e => e.UserName);
        }
    }
}
