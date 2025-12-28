using Microsoft.AspNetCore.Identity;
using DemoBai123.Models;

namespace DemoBai123.Data;

/// <summary>
/// Class để seed dữ liệu ban đầu: Roles và Admin user
/// Dùng chung cho tất cả các Bài (Bai1, Bai2, Bai3)
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seed roles và admin user vào database
    /// </summary>
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Tạo Roles nếu chưa tồn tại
        string[] roleNames = { "Admin", "User" };
        
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Tạo Admin user nếu chưa tồn tại
        var adminUser = await userManager.FindByNameAsync("admin");
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@example.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "123456");
            if (result.Succeeded)
            {
                // Gán role Admin cho user
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
        else
        {
            // Đảm bảo admin user có role Admin
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}

