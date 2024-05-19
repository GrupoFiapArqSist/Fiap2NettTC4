using AutoMapper;
using ComandaPro.CrossCutting.Notifications;
using ComandaPro.Domain.Dtos.Default;
using ComandaPro.Domain.Extensions;
using ComandaPro.Service.Services;
using Order.Domain.Dtos;
using Order.Domain.Enums;
using Order.Domain.Filter;
using Order.Domain.Interfaces.Repositories;
using Order.Domain.Interfaces.Services;
using Order.Service.Validators.Order;

namespace Order.Service.Services;

public class OrderService(IMapper mapper, IOrderRepository orderRepository, NotificationContext notificationContext, IOrderItemsRepository orderItemsRepository) : BaseService, IOrderService
{
    private readonly IMapper _mapper = mapper;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly NotificationContext _notificationContext = notificationContext;
    private readonly IOrderItemsRepository _orderItemsRepository = orderItemsRepository;

    public async Task<DefaultServiceResponseDto> AddOrder(AddOrderDto addOrderDto, int userId)
    {
        var validationResult = Validate(addOrderDto, Activator.CreateInstance<AddOrderValidator>());
        if (!validationResult.IsValid) { _notificationContext.AddNotifications(validationResult.Errors); return default; }

        foreach (var item in addOrderDto.OrderItems)
        {
            validationResult = Validate(item, Activator.CreateInstance<AddOrderValidatorItemsValidator>());
            if (!validationResult.IsValid)
            {
                _notificationContext.AddNotifications(validationResult.Errors);
                return default;
            }
        }

        var newOrderDb = _mapper.Map<Domain.Entities.Order>(addOrderDto);

        newOrderDb.OrderItems.ToList().ForEach(item => newOrderDb.ValueTotal += item.ValueUnit * item.Amount);
        newOrderDb.Status = OrderStatusEnum.InPreparation;
        newOrderDb.UserId = userId;

        await _orderRepository.InsertWithReturnId(newOrderDb);

        foreach (var orderItems in newOrderDb.OrderItems)
        {
            orderItems.OrderId = newOrderDb.Id;
            await _orderItemsRepository.Insert(orderItems);
        }

        if (newOrderDb.Id > 0)
            return new DefaultServiceResponseDto()
            {
                //Message = StaticNotifications.OrderSucessWaitingPayment.Message,
                Message = StaticNotifications.OrderSuccess.Message,
                Success = true
            };
        else
            return new DefaultServiceResponseDto()
            {
                Message = StaticNotifications.OrderError.Message,
                Success = false
            };
    }

    public async Task<OrderDetailsDto> GetOrderByCommandId(OrderCommandFilter orderFilter)
    {
        var ltOrderDb = (await _orderRepository.Select())
          .AsQueryable()
          .OrderByDescending(p => p.CreatedAt)
          .ApplyFilter(orderFilter)
          .Where(db => db.CommandId.Equals(orderFilter.CommandId))
          .ToList();

        OrderDetailsDto ltOrderDetails = new()
        {
            Orders = []
        };

        if (ltOrderDb != null)
        {
            ltOrderDetails.Orders.AddRange(_mapper.Map<List<OrderDto>>(ltOrderDb));
            foreach (var orderDto in ltOrderDetails.Orders)
            {
                orderDto.ValueTotal = Math.Round(orderDto.ValueTotal, 2);
                orderDto.StatusDescription = orderDto.Status.GetEnumDescription();
            }
        }

        return ltOrderDetails;
    }

    public async Task<OrderDto> GetOrderById(OrderIdFilter orderFilter)
    {
        var orderDetails = _mapper.Map<OrderDto>(await _orderRepository.Select(orderFilter.OrderId));

        orderDetails.ValueTotal = Math.Round(orderDetails.ValueTotal, 2);
        orderDetails.StatusDescription = orderDetails.Status.GetEnumDescription();

        return orderDetails;
    }

    public async Task<DefaultServiceResponseDto> DeleteOrderByCommandId(OrderCommandFilter orderFilter)
    {

        var orderDb = (await _orderRepository.Select(db => db.CommandId.Equals(orderFilter.CommandId)))?.FirstOrDefault();

        if (orderDb is null)
        {
            return new DefaultServiceResponseDto()
            {
                Message = StaticNotifications.OrderNotFound.Message,
                Success = false
            };
        }
        else
        {
            await _orderRepository.Delete(orderDb.Id);

            return new DefaultServiceResponseDto()
            {
                Message = StaticNotifications.OrderDeleteCommandSucess.Message,
                Success = true
            };
        }
    }

    public async Task<DefaultServiceResponseDto> DeleteOrderById(OrderIdFilter orderFilter)
    {
        var orderDb = await _orderRepository.Select(orderFilter.OrderId);

        if (orderDb is null)
        {
            return new DefaultServiceResponseDto()
            {
                Message = StaticNotifications.OrderNotFound.Message,
                Success = false
            };
        }
        else
        {
            await _orderRepository.Delete(orderDb.Id);

            return new DefaultServiceResponseDto()
            {
                Message = StaticNotifications.OrderDeleteSucess.Message,
                Success = true
            };
        }
    }

    public async Task<DefaultServiceResponseDto> DeleteOrderItemsByOrderId(OrderItemsFilter orderItemsFilter)
    {
        var orderItemDb = (await _orderItemsRepository.Select(db => db.OrderId.Equals(orderItemsFilter.OrderId) && db.ProductId.Equals(orderItemsFilter.ProductId)))?.FirstOrDefault();

        if (orderItemDb is null)
        {
            return new DefaultServiceResponseDto()
            {
                Message = StaticNotifications.OrderNotFound.Message,
                Success = false
            };
        }
        else
        {
            orderItemDb.Order.ValueTotal -= orderItemDb.ValueUnit * orderItemDb.Amount;

            await _orderRepository.Update(orderItemDb.Order);
            await _orderItemsRepository.Delete(orderItemDb.Id);

            return new DefaultServiceResponseDto()
            {
                Message = StaticNotifications.OrderDeleteSucess.Message,
                Success = true
            };
        }
    }

    public async Task<DefaultServiceResponseDto> UpdateAmountItems(UpdateOrderItemsFilter updateOrderItemsFilter)
    {
        var orderItemDb = (await _orderItemsRepository.Select(db => db.OrderId.Equals(updateOrderItemsFilter.OrderId) && db.ProductId.Equals(updateOrderItemsFilter.ProductId)))?.FirstOrDefault();
        DefaultServiceResponseDto response = new();

        if (orderItemDb is null)
        {
            response.Message = StaticNotifications.OrderNotFound.Message;
            response.Success = false;
        }
        else
        {
            orderItemDb.Order.ValueTotal -= orderItemDb.ValueUnit * orderItemDb.Amount;

            if (updateOrderItemsFilter.Amount > 0)
            {
                orderItemDb.Amount = updateOrderItemsFilter.Amount;
                orderItemDb.Order.ValueTotal += orderItemDb.ValueUnit * orderItemDb.Amount;

                response.Message = StaticNotifications.OrderItemsUpdateSucess.Message;
                response.Success = true;
            }
            else
            {
                await _orderItemsRepository.Delete(orderItemDb.Id);

                response.Message = StaticNotifications.OrderDeleteSucess.Message;
                response.Success = true;
            }

            await _orderRepository.Update(orderItemDb.Order);
        }

        return response;
    }
}
