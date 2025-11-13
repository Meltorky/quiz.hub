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

            if (await userManager.FindByEmailAsync(superAdmin.Email) is null)
                await userManager.CreateAsync(superAdmin, "Ab123456+");           
         
            if(!await userManager.IsInRoleAsync(superAdmin,RoleEnums.SuperAdmin.ToString()))
                await userManager.AddToRoleAsync(superAdmin, RoleEnums.SuperAdmin.ToString());
               
            if(!await userManager.IsInRoleAsync(superAdmin, RoleEnums.Admin.ToString()))
                await userManager.AddToRoleAsync(superAdmin, RoleEnums.Admin.ToString());
        } 
    }
}
