using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    /// <summary>
    /// Mapping the Entity with Database Table using FluentAPI.
    /// </summary>
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Configure database table schema.
        /// Note: To use HasColumnType extension install Microsoft.EntityFrameworkCore.Relational package
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.Property(p => p.Image)
                .IsRequired()
                .HasColumnType("varchar(100)");
        }
    }
}
