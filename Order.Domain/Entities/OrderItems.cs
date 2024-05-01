using ComandaPro.Domain.Entities;
using ComandaPro.Domain.Interfaces.Entities;

namespace Order.Domain.Entities;

public class OrderItems : BaseEntity, IEntity<int>
{
    public int ProductId { get; set; }

    public int OrderId { get; set; }

    public int Amount { get; set; }

    public double ValueUnit { get; set; }

    public virtual Order Order { get; set; }
}
