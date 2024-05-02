using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Order.Infra.Data.Mapping;

public class OrderItemsMap : IEntityTypeConfiguration<Domain.Entities.OrderItems>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.OrderItems> builder)
    {
        builder.ToTable("OrderItems");
        builder.HasKey(prop => prop.Id);

        builder.Property(p => p.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

        builder.Property(p => p.Amount)
                .HasColumnName("Amount")
                .HasColumnType("INT")
                .IsRequired();
        
        builder.Property(p => p.ValueUnit)
                .HasColumnName("ValueUnit")
                .HasColumnType("float")
                .IsRequired();
    }
}
