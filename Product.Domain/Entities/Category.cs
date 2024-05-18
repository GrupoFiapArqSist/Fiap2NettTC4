using ComandaPro.Domain.Entities;
using ComandaPro.Domain.Interfaces.Entities;

namespace Product.Domain.Entities
{
    public class Category : BaseEntity, IEntity<int>
    {
        public string Name { get; set; }

        public int UserId { get; set; }

        public bool IsActive { get; set; }
    }
}
