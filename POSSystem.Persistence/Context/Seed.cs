using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using POSSystem.Domain.Entities;

namespace POSSystem.Persistence.Context
{
    public class Seed
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var emailAdmin = "admin@admin.com";
            if (await userManager.FindByEmailAsync(emailAdmin) == null)
            {
                var user = new ApplicationUser
                {
                    Email = emailAdmin,
                    UserName = "Admin"
                };

                await userManager.CreateAsync(user, "Admin123!");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}