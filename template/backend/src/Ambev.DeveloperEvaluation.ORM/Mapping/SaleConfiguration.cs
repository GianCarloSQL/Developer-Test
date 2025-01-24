using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            // Table name
            builder.ToTable("Sales");

            // Primary key
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            // SaleNumber
            builder.Property(s => s.SaleNumber)
                   .IsRequired();

            // SaleDate
            builder.Property(s => s.SaleDate)
                   .IsRequired();

            // Customer
            builder.Property(s => s.Customer)
                   .IsRequired()
                   .HasMaxLength(100);

            // Branch
            builder.Property(s => s.Branch)
                   .IsRequired()
                   .HasMaxLength(50);

            // TotalAmount
            builder.Property(s => s.TotalAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // SaleStatus
            builder.Property(s => s.SaleStatus)
                   .HasConversion<string>()
                   .IsRequired()
                   .HasMaxLength(20);

            // Relationship: Sale -> SaleItems
            builder.HasMany(s => s.SaleItems)
                   .WithOne()
                   .HasForeignKey(si => si.Id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}