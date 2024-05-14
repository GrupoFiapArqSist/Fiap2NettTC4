using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Infra.Data.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Domain.Entities.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(prop => prop.Id);

            builder.Property(o => o.Name)
              .HasColumnName("Name")
              .HasColumnType("VARCHAR(100)")
              .IsRequired();

            builder.Property(c => c.UserId)
            .HasColumnName("UserId")
            .HasColumnType("int")
            .IsRequired();

            builder.Property(o => o.Description)
               .HasColumnName("Description")
               .HasColumnType("VARCHAR(500)")
               .IsRequired();

            builder.Property(p => p.Price)
              .HasColumnName("Price")
              .HasColumnType("float")
              .IsRequired();

            builder.Property(p => p.CategoryId)
               .HasColumnName("CategoryId")
               .IsRequired();

            builder.HasOne(p => p.Category)
               .WithMany()
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.IsActive)
               .HasColumnName("IsActive")
               .HasColumnType("bit")
               .IsRequired();

            builder.Property(p => p.CreatedAt)
                    .HasColumnName("CreatedAt")
                    .HasColumnType("DATETIME")
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();
        }
    }
}
