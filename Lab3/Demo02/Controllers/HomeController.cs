using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Demo02.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Demo02.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<AppUser> _userManager;

    public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Secured()
    {
        var user = await _userManager.GetUserAsync(User);
        ViewBag.UserName = user?.UserName;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
