using Demo01.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Demo01.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public AdminController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    // LIST USERS
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        // Just passing users list directly for simple demo, or can use ViewModel
        return View(users);
    }

    // MANAGE CLAIMS UI
    public async Task<IActionResult> Claims(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound("User not found");

        var claims = await _userManager.GetClaimsAsync(user);
        
        var model = new UserClaimsViewModel
        {
            UserId = user.Id,
            UserEmail = user.Email,
            Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value }).ToList()
        };

        return View(model);
    }

    // ADD CLAIM
    [HttpPost]
    public async Task<IActionResult> AddClaim(string userId, string type, string value)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound("User not found");

        // Simple validation
        if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(value))
        {
            TempData["Error"] = "Type and Value are required.";
            return RedirectToAction("Claims", new { userId });
        }

        var existingClaims = await _userManager.GetClaimsAsync(user);
        if (existingClaims.Any(c => c.Type == type && c.Value == value))
        {
            TempData["Error"] = "This claim already exists for the user.";
            return RedirectToAction("Claims", new { userId });
        }

        var result = await _userManager.AddClaimAsync(user, new Claim(type, value));
        if (result.Succeeded)
        {
            TempData["Success"] = "Claim added successfully.";
        }
        else
        {
            TempData["Error"] = "Error adding claim.";
        }

        return RedirectToAction("Claims", new { userId });
    }

    // REMOVE CLAIM
    [HttpPost]
    public async Task<IActionResult> RemoveClaim(string userId, string type, string value)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound("User not found");

        var result = await _userManager.RemoveClaimAsync(user, new Claim(type, value));
         if (result.Succeeded)
        {
            TempData["Success"] = "Claim removed successfully.";
        }
        else
        {
            TempData["Error"] = "Error removing claim.";
        }

        return RedirectToAction("Claims", new { userId });
    }
}
