using ComandaPro.Domain.Filters;

namespace Order.Domain.Filter;

public class OrderCommandFilter : _BaseFilter
{
    public int CommandId { get; set; }
}
