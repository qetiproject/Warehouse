using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.DomainModels.Models;

namespace Warehouse.ApplicationService.Extensions
{
    public class Seed
    {
        public static async Task SeedUSers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var roles = new List<Role>
            {
                new Role{ Name = "Administrator" }
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var admin = new User
            {
                UserName = "admin"
            };

        }
    }
}
