using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bai4.Models;
using bai4.Areas.Admin.ViewModels;

namespace bai4.Areas.Admin.Controllers;

[Area("Admin")]
// [Authorize(Roles = "Admin")] // Uncomment in real app
public class DashboardController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DashboardController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel
        {
            TotalUsers = await _userManager.Users.CountAsync(),
            TotalRoles = await _roleManager.Roles.CountAsync(),
            UnconfirmedUsers = await _userManager.Users.CountAsync(u => !u.EmailConfirmed),
            // NewUsersToday: IdentityUser doesn't have CreatedDate by default. 
            // For demo, we leave it as 0 or mock it if we extended the user.
            NewUsersToday = 0 
        };

        return View(model);
    }
}
