using Demo02.Models;
using Microsoft.AspNetCore.Identity;

namespace Demo02.Data;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        string email = "admin@example.com";
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new AppUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, "Admin@123"); // Mật khẩu mẫu
        }
    }
}
