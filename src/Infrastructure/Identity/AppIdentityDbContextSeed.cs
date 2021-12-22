using ApplicationCore.Constans;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            if (await roleManager.Roles.AnyAsync() || await userManager.Users.AnyAsync()) return;
            var adminEmail = "admin@example.com";
            var userEmail = "user@example.com";

            await roleManager.CreateAsync(new IdentityRole() { Name = AuthorizationConstants.Roles.ADMIN });

            var adminUser = new ApplicationUser() { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
            await userManager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.ADMIN);

            var demoUser = new ApplicationUser() { Email = userEmail, UserName = userEmail, EmailConfirmed = true };
            await userManager.CreateAsync(demoUser, AuthorizationConstants.DEFAULT_PASSWORD);

        }
    }
}
