using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Writers;
using quiz.hub.Domain.Identity;
using quiz.hub.Infrastructure.Seeder;

namespace quiz.hub.API.Extentions
{
    public static class IdentitySeederExtention
    {
        public static async Task SeedIdentityAsync(this IServiceProvider service)
        {
            using var scope = service.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await DefaultRoles.SeedAsync(roleManager);
            await DefaultUsers.SeedAsync(userManager);
        }
    }
}
