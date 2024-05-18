using ComandaPro.Domain.Entities;
using ComandaPro.Domain.Interfaces.Entities;

namespace Product.Domain.Entities
{
    public class Product : BaseEntity, IEntity<int>
    {
        public string Name { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public bool IsActive { get; set; }

        public virtual Category Category { get; set; }

    }
}
