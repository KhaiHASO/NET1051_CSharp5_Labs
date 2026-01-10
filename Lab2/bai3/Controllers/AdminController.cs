using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bai3.Models;
using bai3.ViewModels;

namespace bai3.Controllers;

public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        var userRolesViewModel = new List<UserRolesViewModel>();

        foreach (var user in users)
        {
            var thisViewModel = new UserRolesViewModel();
            thisViewModel.UserId = user.Id;
            thisViewModel.UserName = user.UserName;
            thisViewModel.Email = user.Email;
            thisViewModel.Roles = await _userManager.GetRolesAsync(user);
            userRolesViewModel.Add(thisViewModel);
        }

        return View(userRolesViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> ManageRoles(string userId)
    {
        ViewBag.userId = userId;

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
            return View("NotFound");
        }

        ViewBag.UserName = user.UserName;

        var model = new List<ManageUserRolesViewModel>();

        foreach (var role in _roleManager.Roles.ToList())
        {
            var userRolesViewModel = new ManageUserRolesViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };

            if (await _userManager.IsInRoleAsync(user, role.Name))
            {
                userRolesViewModel.IsSelected = true;
            }
            else
            {
                userRolesViewModel.IsSelected = false;
            }

            model.Add(userRolesViewModel);
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ManageRoles(List<ManageUserRolesViewModel> model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
            return View("NotFound");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var result = await _userManager.RemoveFromRolesAsync(user, roles);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Cannot remove user existing roles");
            return View(model);
        }

        result = await _userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(y => y.RoleName));

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Cannot add selected roles to user");
            return View(model);
        }

        return RedirectToAction("Index");
    }
    
    // Helper to create role quickly for testing
    [HttpGet]
    public IActionResult CreateRole() => View();

    [HttpPost]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        await _roleManager.CreateAsync(new IdentityRole(roleName));
        return RedirectToAction("Index");
    }
}
