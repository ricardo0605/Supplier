using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        /// <summary>
        /// Configure database table schema.
        /// Note: To use HasColumnType extension install Microsoft.EntityFrameworkCore.Relational package
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Street)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Number)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.ZipCode)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.Property(p => p.AditionalInfo)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Neighborhood)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.City)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.State)
                .IsRequired()
                .HasColumnType("varchar(50)");
        }
    }
}
