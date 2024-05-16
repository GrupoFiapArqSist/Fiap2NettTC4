using ComandaPro.Infra.Data.Repositories;
using Command.Domain.Interfaces.Repositories;
using Command.Infra.Data.Context;

namespace Command.Infra.Data.Repositories
{
	public class CommandRepository(ApplicationDbContext context) : BaseRepository<Domain.Entities.Command, int, ApplicationDbContext>(context), ICommandRepository
	{
		public async Task<Domain.Entities.Command> InsertWithReturnId(Domain.Entities.Command obj)
		{
			_dataContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Added;
			await _dataContext.SaveChangesAsync();

			return obj;
		}
	}
}
