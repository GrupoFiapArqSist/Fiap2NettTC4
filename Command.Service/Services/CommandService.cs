using AutoMapper;
using ComandaPro.CrossCutting.Notifications;
using ComandaPro.Domain.Dtos.Default;
using ComandaPro.Domain.Extensions;
using ComandaPro.Service.Services;
using Command.Domain.Dtos;
using Command.Domain.Enums;
using Command.Domain.Filters;
using Command.Domain.Interfaces.Integration;
using Command.Domain.Interfaces.Repositories;
using Command.Domain.Interfaces.Services;
using Command.Service.Validators;

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
			var dto = new CommandDto { Number = number, UserId = userId, Status = CommandStatusEnum.Open, CreatedAt = DateTime.Now };

			var validationResult = Validate(dto, Activator.CreateInstance<CommandDtoValidator>());
			if (!validationResult.IsValid)
			{
				_notificationContext.AddNotifications(validationResult.Errors);
				return default;
			}

			var existingCommand = await _commandRepository.Select(c => c.Number == number && c.Status == CommandStatusEnum.Open);

			if (existingCommand.FirstOrDefault() != null)
			{
				return new DefaultServiceResponseDto()
				{
					Message = StaticNotifications.CommandIsAlreadyOpened.Message,
					Success = false
				};
			}

			var command = _mapper.Map<Domain.Entities.Command>(dto);
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

		public async Task<IEnumerable<CommandDto>> GetCommands(CommandFilter filter, string accessToken)
		{
			var commands = (await _commandRepository.Select()).AsQueryable().ApplyFilter(filter).Where(c => c.Status == filter.Status).ToList();
			var dtos = _mapper.Map<IEnumerable<CommandDto>>(commands);
			foreach (var dto in dtos)
			{
				var orders = await _orderIntegration.GetOrdersByCommand(dto.Id, accessToken);
				dto.Orders = orders;
				dto.ValueTotalBeforeServiceCharge = orders.Sum(o => o.ValueTotal);
			}
			return dtos;
		}

		public async Task<CommandDto?> CloseCommand(int number, string accessToken)
		{
			var validationResult = Validate(new CommandDto { Number = number }, Activator.CreateInstance<CommandDtoValidator>());
			if (!validationResult.IsValid)
			{
				_notificationContext.AddNotifications(validationResult.Errors);
				return default;
			}

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

			command.ValueTotalBeforeServiceCharge = orders.Sum(o => o.ValueTotal);
			command.ClosedAt = DateTime.Now;
			command.Status = CommandStatusEnum.Closed;
			await _commandRepository.Update(command);

			var dto = _mapper.Map<CommandDto>(command);
			dto.Orders = orders;

			return dto;
		}
	}
}
