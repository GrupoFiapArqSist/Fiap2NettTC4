using ComandaPro.Infra.Data.Repositories;
using Order.Domain.Entities;
using Order.Domain.Interfaces.Repositories;
using Order.Infra.Data.Context;

namespace Order.Infra.Data.Repositories;

public class OrderItemsRepository(ApplicationDbContext context) : BaseRepository<OrderItems, int, ApplicationDbContext>(context), IOrderItemsRepository
{ 

}

