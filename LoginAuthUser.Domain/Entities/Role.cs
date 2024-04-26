using ComandaPro.Domain.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;

namespace LoginAuthUser.Domain.Entities;

public class Role : IdentityRole<int>, IEntity<int>
{
    public Role(string roleName)
    {
        Name = roleName;
        NormalizedName = roleName;
    }
    public Role() { }
}
