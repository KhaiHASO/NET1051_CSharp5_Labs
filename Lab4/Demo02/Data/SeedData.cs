using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Demo02.Data;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await context.Database.MigrateAsync();

            // Seed Roles
            if (!await roleManager.RoleExistsAsync("Admin")) await roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!await roleManager.RoleExistsAsync("User")) await roleManager.CreateAsync(new IdentityRole("User"));

            // Seed Admin (Full Permissions)
            var adminEmail = "admin@demo.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                await userManager.CreateAsync(admin, "Admin@12345");
                await userManager.AddToRoleAsync(admin, "Admin");
                
                // Add all permissions
                var permissions = new[] { "Grades.View", "Grades.Edit", "Reports.View", "AdminPanel.Access" };
                foreach (var p in permissions)
                {
                    await userManager.AddClaimAsync(admin, new Claim("Permission", p));
                }
            }

            // Seed User (Limited Permissions)
            var userEmail = "user@demo.com";
            if (await userManager.FindByEmailAsync(userEmail) == null)
            {
                var user = new IdentityUser { UserName = userEmail, Email = userEmail, EmailConfirmed = true };
                await userManager.CreateAsync(user, "User@12345");
                await userManager.AddToRoleAsync(user, "User");

                // Only Grades.View
                await userManager.AddClaimAsync(user, new Claim("Permission", "Grades.View"));
            }
        }
    }
}
