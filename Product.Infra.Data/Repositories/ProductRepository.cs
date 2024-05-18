using ComandaPro.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Domain.Interfaces.Repositories;
using Product.Infra.Data.Context;

namespace Product.Infra.Data.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : BaseRepository<Domain.Entities.Product, int, ApplicationDbContext>(context), IProductRepository
    {
        public async Task<Domain.Entities.Product> ExistsByName(string name)
        {
            return await _dataContext.Set<Domain.Entities.Product>().FirstOrDefaultAsync(t => t.Name == name);
        }
    }
}
