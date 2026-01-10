using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demo03.Models;
using demo03.ViewModels;

namespace demo03.Controllers;

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        var userViewModels = new List<UserViewModel>();

        foreach (var user in users)
        {
            var thisViewModel = new UserViewModel();
            thisViewModel.Id = user.Id;
            thisViewModel.Email = user.Email;
            thisViewModel.UserName = user.UserName;
            thisViewModel.Roles = await _userManager.GetRolesAsync(user);
            userViewModels.Add(thisViewModel);
        }

        return View(userViewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
            return View("NotFound");
        }

        var model = new EditUserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            Address = user.Address,
            FullName = user.FullName
        };

        var allRoles = _roleManager.Roles.ToList();
        var userRoles = await _userManager.GetRolesAsync(user);

        foreach (var role in allRoles)
        {
            var roleViewModel = new RoleSelectionViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };

            if (userRoles.Contains(role.Name))
            {
                roleViewModel.IsSelected = true;
            }
            else
            {
                roleViewModel.IsSelected = false;
            }

            model.Roles.Add(roleViewModel);
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditUserViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);

        if (user == null)
        {
            ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
            return View("NotFound");
        }
        else
        {
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.FullName = model.FullName;
            user.Address = model.Address;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                
                // Lọc các roles được chọn từ model
                var selectedRoles = model.Roles.Where(x => x.IsSelected).Select(y => y.RoleName).ToList();

                // Roles cần thêm: Selected - Existing
                // Roles cần xóa: Existing - Selected
                
                var resultAdd = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
                if (!resultAdd.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot add selected roles to user");
                    return View(model);
                }

                var resultRemove = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
                if (!resultRemove.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot remove roles from user");
                    return View(model);
                }

                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
            return View("NotFound");
        }
        else
        {
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Index");
        }
    }
    [HttpGet]
    public IActionResult Create()
    {
        var model = new CreateUserViewModel();
        var allRoles = _roleManager.Roles.ToList();

        foreach (var role in allRoles)
        {
            model.Roles.Add(new RoleSelectionViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                IsSelected = false
            });
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                Address = model.Address
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var selectedRoles = model.Roles
                    .Where(x => x.IsSelected)
                    .Select(y => y.RoleName)
                    .ToList();

                if (selectedRoles.Any())
                {
                    var resultAdd = await _userManager.AddToRolesAsync(user, selectedRoles);
                    if (!resultAdd.Succeeded)
                    {
                        ModelState.AddModelError("", "Cannot add selected roles to user");
                        return View(model);
                    }
                }

                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View(model);
    }
}
