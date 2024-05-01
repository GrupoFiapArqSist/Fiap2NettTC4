using Order.Domain.Enums;

namespace Order.Domain.Dtos;

public class OrderDto
{
    public int CommandId { get; set; }

    public int UserId { get; set; }

    public OrderStatusEnum Status { get; set; }

    public string StatusDescription { get; set; }

    public double ValueTotal { get; set; }

    public string Observation { get; set; }

    public virtual ICollection<OrderItemsDto> OrderItems { get; set; }
}
