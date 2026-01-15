using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo02.Controllers;

[Authorize(Policy = "Permission:Grades.View")]
public class GradesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Policy = "Permission:Grades.Edit")]
    public IActionResult Edit()
    {
        return View();
    }
}
