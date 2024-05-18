using ComandaPro.Domain.Interfaces.Repositories;

namespace Command.Domain.Interfaces.Repositories
{
	public interface ICommandRepository : IBaseRepository<Entities.Command, int>
	{
		Task<Entities.Command> InsertWithReturnId(Entities.Command obj);
	}
}
