using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoBai1.Models;

namespace DemoBai1.Controllers;

/// <summary>
/// HomeController - VERSION TRƯỚC KHI THÊM [Authorize] (BƯỚC 4)
/// File này chỉ để tham khảo, so sánh với version sau khi thêm [Authorize]
/// </summary>
public class HomeController_Buoc4 : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Action Secured - BƯỚC 2 & 3: Chưa có [Authorize]
    /// Ai cũng có thể truy cập /Home/Secured mà không cần đăng nhập
    /// </summary>
    [HttpGet]
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

