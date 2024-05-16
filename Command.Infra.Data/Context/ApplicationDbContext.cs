using ComandaPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Command.Infra.Data.Context
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
	{
		public DbSet<Domain.Entities.Command> Command { get; set; }
		public DbSet<SeedHistory> SeedHistories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//modelBuilder.Entity<Domain.Entities.Order>(new OrderMap().Configure);
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies();
		}
	}
}
