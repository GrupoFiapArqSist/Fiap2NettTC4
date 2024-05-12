using Command.Domain.Dtos;

namespace Command.Domain.Interfaces.Integration
{
	public interface IOrderIntegration
	{
		Task<IEnumerable<OrderDto>> GetOrdersByCommand(int commandId, string accessToken);
	}
}
