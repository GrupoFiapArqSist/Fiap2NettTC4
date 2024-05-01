using ComandaPro.Domain.Entities;
using ComandaPro.Domain.Interfaces.Entities;
using Order.Domain.Enums;

namespace Order.Domain.Entities;

public class Order : BaseEntity, IEntity<int>
{
    public int CommandId { get; set; }

    public int UserId { get; set; }

    public OrderStatusEnum Status { get; set; }

    public double ValueTotal { get; set; }

    public string Observation { get; set; }

    public virtual ICollection<OrderItems> OrderItems { get; set; }
}
