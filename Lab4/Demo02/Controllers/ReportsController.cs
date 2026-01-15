using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo02.Controllers;

[Authorize(Policy = "Permission:Reports.View")]
public class ReportsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
