using Microsoft.AspNetCore.Identity;
using quiz.hub.Domain.Enums;
using quiz.hub.Domain.Identity;

namespace quiz.hub.Infrastructure.Seeder
{
    public static class DefaultUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager) 
        {
            ApplicationUser superAdmin = new()
            {
                Email = "superadmin@gmail.com",
                EmailConfirmed = true,
                Name = "Super Admin",
                UserName = "@superadmin",
            };

            var existingUser = await userManager.FindByEmailAsync(superAdmin.Email);

            if (existingUser is null)
            {
                string password = "Ab123456+";
                var isCreated = await userManager.CreateAsync(superAdmin, password);
                if(isCreated.Succeeded)
                    await userManager.AddToRolesAsync(superAdmin, new List<string>
                    {
                        RoleEnums.SuperAdmin.ToString(),
                        RoleEnums.Admin.ToString()
                    });
            }
            else 
            {
                if(!await userManager.IsInRoleAsync(existingUser,RoleEnums.SuperAdmin.ToString()))
                    await userManager.AddToRoleAsync(existingUser, RoleEnums.SuperAdmin.ToString());
               
                if(!await userManager.IsInRoleAsync(existingUser, RoleEnums.Admin.ToString()))
                    await userManager.AddToRoleAsync(existingUser, RoleEnums.Admin.ToString());
            }
        } 
    }
}
