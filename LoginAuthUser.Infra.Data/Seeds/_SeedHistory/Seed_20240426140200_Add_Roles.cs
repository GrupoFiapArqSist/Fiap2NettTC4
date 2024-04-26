using ComandaPro.Domain.Entities;
using ComandaPro.Domain.Utilities;
using LoginAuthUser.Domain.Entities;
using LoginAuthUser.Infra.Data.Context;

namespace LoginAuthUser.Infra.Data.Seeds._SeedHistory;

public class Seed_20240426140200_Add_Roles : Seed
{
    private readonly ApplicationDbContext _dbContext;
    public Seed_20240426140200_Add_Roles(ApplicationDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Up()
    {
        var roles = new List<Role>
        {
            new(StaticUserRoles.USER),
        };

        _dbContext.Roles.AddRange(roles);
        _dbContext.SaveChanges();
    }
}
