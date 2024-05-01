using ComandaPro.Domain.Dtos.Default;
using Order.Domain.Dtos;
using Order.Domain.Filter;

namespace Order.Domain.Interfaces.Services;

public interface IOrderService
{
    Task<DefaultServiceResponseDto> AddOrder(AddOrderDto addOrderDto, int userId, string accessToken);

    Task<OrderDetailsDto> GetOrderByCommandId(OrderCommandFilter orderFilter);

    Task<OrderDto> GetOrderById(OrderIdFilter orderFilter);

    Task<DefaultServiceResponseDto> DeleteOrderByCommandId(OrderCommandFilter orderFilter);

    Task<DefaultServiceResponseDto> DeleteOrderById(OrderIdFilter orderFilter);

    Task<DefaultServiceResponseDto> DeleteOrderItemsByOrderId(OrderItemsFilter orderItemsFilter);

    Task<DefaultServiceResponseDto> UpdateAmountItems(UpdateOrderItemsFilter updateOrderItemsFilter);
}
