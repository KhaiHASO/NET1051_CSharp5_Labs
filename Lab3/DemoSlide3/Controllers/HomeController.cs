using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DemoSlide3.Models;

namespace DemoSlide3.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Action Secured - yêu cầu đăng nhập mới truy cập được
    /// [Authorize] attribute sẽ redirect về trang login nếu chưa đăng nhập
    /// </summary>
    [HttpGet]
    [Authorize]
    public IActionResult Secured()
    {
        return View("Secured", "Hello");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
