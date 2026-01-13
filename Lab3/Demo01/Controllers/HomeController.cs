using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Demo01.Models;
using Microsoft.AspNetCore.Authorization;

namespace Demo01.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [AllowAnonymous] // Cho phép truy cập nặc danh
    public IActionResult Index()
    {
        return View();
    }

    [Authorize] // Yêu cầu phải đăng nhập mới được vào
    public IActionResult Secured()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
