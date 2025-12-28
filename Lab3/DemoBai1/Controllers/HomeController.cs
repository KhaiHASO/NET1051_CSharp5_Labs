using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DemoBai1.Models;

namespace DemoBai1.Controllers;

public class HomeController : Controller
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
    /// Action Secured - BƯỚC 5: Thêm [Authorize] để bảo vệ action
    /// Khi chưa đăng nhập, sẽ tự động redirect về /Identity/Account/Login?returnUrl=/Home/Secured
    /// </summary>
    [HttpGet]
    [Authorize] // BƯỚC 5: Thêm attribute này để yêu cầu đăng nhập
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
