using ComandaPro.Domain.Dtos.Default;
using Command.Domain.Dtos;
using Command.Domain.Filters;

namespace Command.Domain.Interfaces.Services
{
	public interface ICommandService
	{
		Task<DefaultServiceResponseDto> OpenCommand(int number, int userId);

		Task<IEnumerable<CommandDto>> GetCommands(CommandFilter filter, string accessToken);

		Task<CommandDto?> CloseCommand(int number, string accessToken);
	}
}
