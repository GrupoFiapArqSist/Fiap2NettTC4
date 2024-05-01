using ComandaPro.Infra.Data.Repositories;
using Order.Domain.Interfaces.Repositories;
using Order.Infra.Data.Context;

namespace Order.Infra.Data.Repositories;

public class OrderRepository(ApplicationDbContext context) : BaseRepository<Domain.Entities.Order, int, ApplicationDbContext>(context), IOrderRepository
{
    public async Task<Domain.Entities.Order> InsertWithReturnId(Domain.Entities.Order obj)
    {
        _dataContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        await _dataContext.SaveChangesAsync();

        return obj;
    }

}