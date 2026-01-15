using Demo02.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Demo02.Controllers;

[Authorize(Roles = "Admin")] // Or use Permission:AdminPanel.Access
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public AdminController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        // Simple list, just passing IdentityUser directly
        var users = await _userManager.Users.ToListAsync();
        return View(users);
    }

    public async Task<IActionResult> Permissions(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound("User not found");

        var claims = await _userManager.GetClaimsAsync(user);
        var permissions = claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();

        var model = new UserPermissionsViewModel
        {
            UserId = user.Id,
            UserEmail = user.Email,
            Permissions = permissions
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddPermission(string userId, string permission)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        if (string.IsNullOrWhiteSpace(permission))
        {
            TempData["Error"] = "Permission cannot be empty.";
             return RedirectToAction("Permissions", new { userId });
        }

        var claims = await _userManager.GetClaimsAsync(user);
        if (claims.Any(c => c.Type == "Permission" && c.Value == permission))
        {
            TempData["Error"] = "User already has this permission.";
            return RedirectToAction("Permissions", new { userId });
        }

        await _userManager.AddClaimAsync(user, new Claim("Permission", permission));
        TempData["Success"] = "Permission added.";
        return RedirectToAction("Permissions", new { userId });
    }

    [HttpPost]
    public async Task<IActionResult> RemovePermission(string userId, string permission)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        await _userManager.RemoveClaimAsync(user, new Claim("Permission", permission));
        TempData["Success"] = "Permission removed.";
        return RedirectToAction("Permissions", new { userId });
    }
}
