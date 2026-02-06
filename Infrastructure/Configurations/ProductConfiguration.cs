using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // =========================
            // TABLE
            // =========================
            builder.ToTable("Products");

            // =========================
            // PRIMARY KEY
            // =========================
            builder.HasKey(e => e.ProductId);

            builder.Property(e => e.ProductId)
                   .ValueGeneratedOnAdd();

            // =========================
            // COLUMNS
            // =========================
            builder.Property(e => e.ProductName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(e => e.Unit)
                   .HasMaxLength(50);

            builder.Property(e => e.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Stock)
                   .IsRequired();

            builder.Property(e => e.LowStockThreshold)
                   .IsRequired(false);

            builder.Property(e => e.Status)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.CreatedDate)
                   .IsRequired();

            builder.Property(e => e.ModifiedBy)
                   .HasMaxLength(100);

            builder.Property(e => e.ModifiedDate)
                   .IsRequired(false);

            // =========================
            // RELATIONSHIPS
            // =========================
            builder.HasOne<Category>()
                   .WithMany()
                   .HasForeignKey(e => e.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // INDEXES
            // =========================
            builder.HasIndex(e => e.ProductName);
            builder.HasIndex(e => e.CategoryId);
            builder.HasIndex(e => e.Status);
        }
    }
}
