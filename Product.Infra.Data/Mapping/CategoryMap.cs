using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Product.Infra.Data.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Domain.Entities.Category>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(prop => prop.Id);

            builder.Property(c => c.Name)
               .HasColumnName("Name")
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

            builder.Property(c => c.UserId)
           .HasColumnName("UserId")
           .HasColumnType("int")
           .IsRequired();

            builder.Property(c => c.IsActive)
               .HasColumnName("IsActive")
               .HasColumnType("bit")
               .IsRequired();

            builder.Property(c => c.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();
        }
    }
}
