using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Infra.Data.Mapping;

public class OrderMap : IEntityTypeConfiguration<Domain.Entities.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
    {
        builder.ToTable("Order");
        builder.HasKey(prop => prop.Id);

        builder.Property(p => p.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

        builder.Property(o => o.Observation)
                .HasColumnName("Observation")
                .HasColumnType("VARCHAR(500)")
                .IsRequired();

        builder.Property(o => o.ValueTotal)
                .HasColumnName("ValueTotal")
                .HasColumnType("FLOAT")
                .IsRequired();

        builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .HasPrincipalKey(o => o.Id);
    }
}
