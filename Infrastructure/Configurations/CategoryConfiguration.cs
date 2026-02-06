using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // =========================
            // TABLE
            // =========================
            builder.ToTable("Categories");

            // =========================
            // PRIMARY KEY
            // =========================
            builder.HasKey(e => e.CategoryId);

            builder.Property(e => e.CategoryId)
                   .ValueGeneratedOnAdd();

            // =========================
            // COLUMNS
            // =========================
            builder.Property(e => e.CategoryName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Description)
                   .HasMaxLength(250);

            // =========================
            // INDEXES
            // =========================
            builder.HasIndex(e => e.CategoryName)
                   .IsUnique();
        }
    }
}
