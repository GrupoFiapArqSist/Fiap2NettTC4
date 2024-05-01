using ComandaPro.Domain.Interfaces.Repositories;

namespace Order.Domain.Interfaces.Repositories;

public interface IOrderRepository : IBaseRepository<Entities.Order, int>
{
    Task<Entities.Order> InsertWithReturnId(Entities.Order obj);
}
