using ComandaPro.Domain.Dtos.Default;
using Command.Domain.Dtos;

namespace Command.Domain.Interfaces.Services
{
	public interface ICommandService
	{
		Task<DefaultServiceResponseDto> OpenCommand(int number, int userId);

		Task<IEnumerable<CommandDto>> GetOpenCommands();

		Task<CommandDto?> CloseCommand(int number, string accessToken);
	}
}
