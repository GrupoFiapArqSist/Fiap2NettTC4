using ComandaPro.Domain.Interfaces.Repositories;
using Order.Domain.Entities;

namespace Order.Domain.Interfaces.Repositories;

public interface IOrderItemsRepository : IBaseRepository<OrderItems, int>
{

}
