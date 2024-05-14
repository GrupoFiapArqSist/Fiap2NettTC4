using ComandaPro.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Domain.Interfaces.Repositories;
using Product.Infra.Data.Context;

namespace Product.Infra.Data.Repositories
{
    public class CategoryRepository(ApplicationDbContext context) : BaseRepository<Category, int, ApplicationDbContext>(context), ICategoryRepository
    {
        public async Task<Category> ExistsActiveCategory(string name)
        {
            return await _dataContext.Set<Category>()
                .FirstOrDefaultAsync(t => t.Name == name && t.IsActive);
        }
    }
}
