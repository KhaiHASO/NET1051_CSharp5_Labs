using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DemoBai123.Models;
using DemoBai123.Features.Bai1_Register;
using DemoBai123.Features.Bai2_Auth_ChangePassword;

namespace DemoBai123.Controllers;

/// <summary>
/// Controller xử lý Account - Bao gồm Bài 1 (Register) và Bài 2 (Login, ChangePassword)
/// </summary>
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    #region Bài 1: Register (UserManager)

    /// <summary>
    /// Hiển thị form đăng ký - Bài 1
    /// </summary>
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    /// <summary>
    /// Xử lý đăng ký user mới - Bài 1
    /// - Check trùng username/email
    /// - CreateAsync
    /// - Thành công redirect sang Login
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVm model)
    {
        if (ModelState.IsValid)
        {
            // Check trùng username
            var existingUserByUsername = await _userManager.FindByNameAsync(model.Username);
            if (existingUserByUsername != null)
            {
                ModelState.AddModelError(string.Empty, "Tên đăng nhập đã tồn tại.");
                return View(model);
            }

            // Check trùng email
            var existingUserByEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existingUserByEmail != null)
            {
                ModelState.AddModelError(string.Empty, "Email đã được sử dụng.");
                return View(model);
            }

            // Tạo user mới
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            // CreateAsync - Bài 1
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Tự động gán role "User" cho user mới
                await _userManager.AddToRoleAsync(user, "User");
                
                // Thành công redirect sang Login - Bài 1
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    #endregion

    #region Bài 2: Login + ChangePassword

    /// <summary>
    /// Hiển thị form đăng nhập - Bài 2
    /// </summary>
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    /// <summary>
    /// Xử lý đăng nhập - Bài 2
    /// Login bằng SignInManager
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVm model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (ModelState.IsValid)
        {
            // Login bằng SignInManager - Bài 2
            var result = await _signInManager.PasswordSignInAsync(
                model.Username, 
                model.Password, 
                isPersistent: false, 
                lockoutOnFailure: false);
            
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin.");
        }

        return View(model);
    }

    /// <summary>
    /// Đăng xuất - Bài 2
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    /// <summary>
    /// Hiển thị form đổi mật khẩu - Bài 2
    /// ChangePassword có [Authorize]
    /// </summary>
    [HttpGet]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public IActionResult ChangePassword()
    {
        return View();
    }

    /// <summary>
    /// Xử lý đổi mật khẩu - Bài 2
    /// Dùng UserManager.ChangePasswordAsync
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordVm model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        // Dùng UserManager.ChangePasswordAsync - Bài 2
        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (result.Succeeded)
        {
            // Đăng nhập lại để cập nhật cookie
            await _signInManager.RefreshSignInAsync(user);
            ViewData["SuccessMessage"] = "Đổi mật khẩu thành công!";
            return View();
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    #endregion
}

