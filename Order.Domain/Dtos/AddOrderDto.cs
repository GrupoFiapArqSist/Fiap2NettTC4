namespace Order.Domain.Dtos;

public class AddOrderDto
{
    public int CommandId { get; set; }

    public string Observation { get; set; }

    public virtual ICollection<OrderItemsDto> OrderItems { get; set; }
}

