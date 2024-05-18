using ComandaPro.Domain.Interfaces.Repositories;
using Product.Domain.Entities;

namespace Product.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category, int>
    {
        Task<Category> ExistsActiveCategory(string name);
    }
}
