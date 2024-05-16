using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Command.Infra.Data.Mapping
{
	public class CommandMap : IEntityTypeConfiguration<Domain.Entities.Command>
	{
		public void Configure(EntityTypeBuilder<Domain.Entities.Command> builder)
		{
			builder.ToTable("Command");
			builder.HasKey(prop => prop.Id);

			builder.Property(p => p.CreatedAt)
					.HasDefaultValueSql("GETDATE()")
					.IsRequired();
		}
	}
}
