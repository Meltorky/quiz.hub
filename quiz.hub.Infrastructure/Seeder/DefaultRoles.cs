using Microsoft.AspNetCore.Identity;
using quiz.hub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Infrastructure.Seeder
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            List<string> roles = Enum.GetNames(typeof(RoleEnums)).ToList();
            foreach (string role in roles)
            {
                if (await roleManager.FindByNameAsync(role) is null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = role });
            }
        }
    }
}
