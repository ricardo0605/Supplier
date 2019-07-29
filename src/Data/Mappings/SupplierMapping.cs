using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    /// <summary>
    /// Mapping the Entity with Database Table using FluentAPI.
    /// </summary>
    public class SupplierMapping : IEntityTypeConfiguration<Supplier>
    {
        /// <summary>
        /// Configure database table schema.
        /// Note: To use HasColumnType extension install Microsoft.EntityFrameworkCore.Relational package
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.DocumentNumber)
                .IsRequired()
                .HasColumnType("varchar(14)");

            // One-To-One relationship between Supplier and Address
            builder.HasOne(s => s.Address)
                .WithOne(a => a.Supplier);

            // One-To-Many relationship between Supplier and Products
            builder.HasMany(s => s.Products)
                .WithOne(p => p.Supplier)
                .HasForeignKey(s => s.SupplierId);
        }
    }
}
