using ComandaPro.Infra.Data.Repositories;
using LoginAuthUser.Domain.Entities;
using LoginAuthUser.Domain.Interfaces.Repositories;
using LoginAuthUser.Infra.Data.Context;

namespace LoginAuthUser.Infra.Data.Repositories;

public class UserRepository(ApplicationDbContext context) : BaseRepository<User, int, ApplicationDbContext>(context), IUserRepository
{
}