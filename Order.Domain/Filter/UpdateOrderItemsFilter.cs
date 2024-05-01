namespace Order.Domain.Filter;

public class UpdateOrderItemsFilter
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
}
