using Demo01.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo01.Controllers;

[Authorize]
public class ClaimsController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public ClaimsController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> My()
    {
        var user = await _userManager.GetUserAsync(User);
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
}
