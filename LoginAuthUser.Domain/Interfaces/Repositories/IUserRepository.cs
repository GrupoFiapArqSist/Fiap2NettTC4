using ComandaPro.Domain.Interfaces.Repositories;
using LoginAuthUser.Domain.Entities;

namespace LoginAuthUser.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User, int>
{
}

