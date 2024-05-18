using AutoMapper;
using ComandaPro.CrossCutting.Notifications;
using ComandaPro.Domain.Dtos.Default;
using ComandaPro.Service.Services;
using Command.Domain.Dtos;
using Command.Domain.Enums;
using Command.Domain.Interfaces.Integration;
using Command.Domain.Interfaces.Repositories;
using Command.Domain.Interfaces.Services;

namespace Command.Service.Services
{
	public class CommandService(IMapper mapper, ICommandRepository commandRepository, IOrderIntegration orderIntegration, NotificationContext notificationContext) : BaseService, ICommandService
	{
		private readonly IMapper _mapper = mapper;
		private readonly ICommandRepository _commandRepository = commandRepository;
		private readonly IOrderIntegration _orderIntegration = orderIntegration;
		private readonly NotificationContext _notificationContext = notificationContext;

		public async Task<DefaultServiceResponseDto> OpenCommand(int number, int userId)
		{
			var existingCommand = await _commandRepository.Select(c => c.Number == number && c.Status == CommandStatusEnum.Open);

			if (existingCommand.FirstOrDefault() != null)
			{
				return new DefaultServiceResponseDto()
				{
					Message = StaticNotifications.CommandIsAlreadyOpened.Message,
					Success = false
				};
			}

			var command = new Domain.Entities.Command { Number = number, UserId = userId, Status = CommandStatusEnum.Open };
			command = await _commandRepository.InsertWithReturnId(command);

			if (command == null)
			{
				return new DefaultServiceResponseDto()
				{
					Message = StaticNotifications.CommandError.Message,
					Success = false
				};
			}

			return new DefaultServiceResponseDto()
			{
				Message = StaticNotifications.CommandOpenedSuccess.Message,
				Success = true
			};
		}

		public async Task<CommandDto?> CloseCommand(int number, string accessToken)
		{
			var command = (await _commandRepository.Select(c => c.Number == number)).SingleOrDefault();

			if (command == null)
			{
				_notificationContext.AddNotification(StaticNotifications.CommandNotFound);
				return default;
			}

			if (command.Status == CommandStatusEnum.Closed)
			{
				_notificationContext.AddNotification(StaticNotifications.CommandIsAlreadyClosed);
				return default;
			}

			var orders = await _orderIntegration.GetOrdersByCommand(command.Id, accessToken);

			command.ValueTotal = orders.Sum(o => o.ValueTotal);
			command.ServiceChage = command.ValueTotal * 0.1m;
			command.ValueTotal += command.ServiceChage;
			command.ClosedAt = DateTime.Now;
			command.Status = CommandStatusEnum.Closed;
			await _commandRepository.Update(command);

			var dto = _mapper.Map<CommandDto>(command);
			dto.Orders = orders;

			return dto;
		}
	}
}
