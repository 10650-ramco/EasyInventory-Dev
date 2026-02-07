using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.CustomerCode)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CustomerName)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(c => c.CustomerType)
                .HasMaxLength(100);

            builder.Property(c => c.GSTIN)
                .HasMaxLength(30);

            builder.Property(c => c.PAN)
                .HasMaxLength(20);

            builder.Property(c => c.GSTRegistrationType)
                .HasMaxLength(100);

            builder.Property(c => c.PlaceOfSupplyState)
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .HasMaxLength(400);

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(40);

            builder.Property(c => c.CreatedBy)
                .HasMaxLength(100);

            builder.Property(c => c.ModifiedBy)
                .HasMaxLength(100);
        }
    }
}
