using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo01.Controllers;

[Authorize(Policy = "RequireCanGrade")]
public class GradesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
