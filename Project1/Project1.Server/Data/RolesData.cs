using Microsoft.AspNetCore.Identity;
using Project1.Shared;

namespace Project1.Server.Data
{
    public static class RolesData
    {
        public static async Task SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(AppRoles.User))
            {
                var create = await roleManager.CreateAsync(new IdentityRole<Guid>(AppRoles.User));
            }

            if (!await roleManager.RoleExistsAsync(AppRoles.Admin))
            {
                var create = await roleManager.CreateAsync(new IdentityRole<Guid>(AppRoles.Admin));
            }
        }
    }
}
