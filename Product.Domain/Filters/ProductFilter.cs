using ComandaPro.Domain.Filters;

namespace Product.Domain.Filters
{
    public class ProductFilter : _BaseFilter
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
