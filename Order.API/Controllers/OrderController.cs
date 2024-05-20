using ComandaPro.CrossCutting.Notifications;
using ComandaPro.Domain.Dtos.Default;
using ComandaPro.Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Domain.Dtos;
using Order.Domain.Filter;
using Order.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Order.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : Controller
{
    private readonly IOrderService _orderService = orderService;

    [HttpPost]
    [Route("create-orders")]
    [SwaggerOperation(Summary = "Create orders")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> AddOrder([FromBody] AddOrderDto addOrdeDto)
    {
        var order = await _orderService.AddOrder(addOrdeDto, this.GetUserIdLogged());

        return Ok(order);
    }

    [HttpGet]
    [Route("get-orders-command")]
    [SwaggerOperation(Summary = "get orders by command id")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(OrderDetailsDto))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetOrderByCommand([FromQuery] OrderCommandFilter filter)
    {
        var ltOrder = await _orderService.GetOrderByCommandId(filter);

        if (ltOrder is null || ltOrder.Orders.Count == 0) return NotFound(new DefaultServiceResponseDto() { Message = StaticNotifications.OrderNotFound.Message, Success = true });

        return Ok(ltOrder);
    }

    [HttpGet]
    [Route("get-order-id")]
    [SwaggerOperation(Summary = "get orders by order id")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(OrderDetailsDto))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetOrderById([FromQuery] OrderIdFilter filter)
    {
        var orderDto = await _orderService.GetOrderById(filter);

        if (orderDto is null) return NotFound(new DefaultServiceResponseDto() { Message = StaticNotifications.OrderNotFound.Message, Success = true });

        return Ok(orderDto);
    }

    [HttpDelete]
    [Route("delete-orders-command-id")]
    [SwaggerOperation(Summary = "delete orders by command id")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteOrderByCommandId([FromQuery] OrderCommandFilter filter)
    {
        var response = await _orderService.DeleteOrderByCommandId(filter);

        if (!response.Success) return NotFound(response);
        
        return Ok(response);
    }

    [HttpDelete]
    [Route("delete-order-id")]
    [SwaggerOperation(Summary = "delete order by order id")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteOrderById([FromQuery] OrderIdFilter filter)
    {
        var response = await _orderService.DeleteOrderById(filter);

        if (!response.Success) return NotFound(response);

        return Ok(response);
    }

    [HttpDelete]
    [Route("delete-order-items-id")]
    [SwaggerOperation(Summary = "delete order items by order id")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteOrderById([FromQuery] OrderItemsFilter filter)
    {
        var response = await _orderService.DeleteOrderItemsByOrderId(filter);

        if (!response.Success) return NotFound(response);

        return Ok(response);
    }

    [HttpPut]
    [Route("update-order-items-amount")]
    [SwaggerOperation(Summary = "update order items amount by order id")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateAmountItems([FromQuery] UpdateOrderItemsFilter filter)
    {
        var response = await _orderService.UpdateAmountItems(filter);

        if (!response.Success) return NotFound(response);

        return Ok(response);
    }
}
