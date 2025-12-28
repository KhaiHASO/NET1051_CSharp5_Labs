using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoSlide2.Models;
using DemoSlide2.ViewModels;

namespace DemoSlide2.Controllers;

/// <summary>
/// Controller quản lý Roles và phân quyền (chỉ Admin mới truy cập được)
/// </summary>
[Authorize(Roles = "Admin")]
public class AdministrationController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdministrationController(
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    #region Role Management

    /// <summary>
    /// Hiển thị danh sách tất cả roles
    /// </summary>
    [HttpGet]
    public IActionResult ListRoles()
    {
        var roles = _roleManager.Roles;
        return View(roles);
    }

    /// <summary>
    /// Hiển thị form tạo role mới
    /// </summary>
    [HttpGet]
    public IActionResult CreateRole()
    {
        return View();
    }

    /// <summary>
    /// Xử lý tạo role mới
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRole(CreateRoleVm model)
    {
        if (ModelState.IsValid)
        {
            var identityRole = new IdentityRole
            {
                Name = model.RoleName
            };

            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    /// <summary>
    /// Hiển thị form chỉnh sửa role
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> EditRole(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        var model = new EditRoleVm
        {
            Id = role.Id,
            RoleName = role.Name
        };

        // Lấy danh sách users trong role này
        foreach (var user in _userManager.Users.ToList())
        {
            if (await _userManager.IsInRoleAsync(user, role.Name))
            {
                model.Users.Add(user.UserName ?? string.Empty);
            }
        }

        return View(model);
    }

    /// <summary>
    /// Xử lý cập nhật role
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRole(EditRoleVm model)
    {
        if (string.IsNullOrEmpty(model.Id))
        {
            return NotFound();
        }

        var role = await _roleManager.FindByIdAsync(model.Id);
        if (role == null)
        {
            return NotFound();
        }

        role.Name = model.RoleName;
        var result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            return RedirectToAction("ListRoles");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    /// <summary>
    /// Hiển thị form xác nhận xóa role
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> DeleteRole(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        return View(role);
    }

    /// <summary>
    /// Xử lý xóa role
    /// </summary>
    [HttpPost, ActionName("DeleteRole")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteRoleConfirmed(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        var result = await _roleManager.DeleteAsync(role);
        if (result.Succeeded)
        {
            return RedirectToAction("ListRoles");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(role);
    }

    #endregion

    #region User Role Management

    /// <summary>
    /// Hiển thị form quản lý roles của user
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> EditUsersInRole(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var model = new UserRoleVm
        {
            UserId = user.Id,
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty
        };

        foreach (var role in _roleManager.Roles.ToList())
        {
            var userRoleVm = new RoleCheckboxVm
            {
                RoleId = role.Id,
                RoleName = role.Name ?? string.Empty,
                IsSelected = await _userManager.IsInRoleAsync(user, role.Name ?? string.Empty)
            };
            model.Roles.Add(userRoleVm);
        }

        return View(model);
    }

    /// <summary>
    /// Xử lý cập nhật roles của user
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUsersInRole(UserRoleVm model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return NotFound();
        }

        // Lấy danh sách roles hiện tại của user
        var userRoles = await _userManager.GetRolesAsync(user);

        // Xóa tất cả roles hiện tại
        var result = await _userManager.RemoveFromRolesAsync(user, userRoles);
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Không thể xóa roles hiện tại của user");
            return View(model);
        }

        // Thêm các roles được chọn
        var rolesToAdd = model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName).ToList();
        if (rolesToAdd.Any())
        {
            result = await _userManager.AddToRolesAsync(user, rolesToAdd);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Không thể thêm roles cho user");
                return View(model);
            }
        }

        return RedirectToAction("Index", "Users");
    }

    #endregion
}

