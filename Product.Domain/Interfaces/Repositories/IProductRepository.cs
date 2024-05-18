using ComandaPro.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Entities.Product, int>
    {
        Task<Entities.Product> ExistsByName(string name);
    }
}
