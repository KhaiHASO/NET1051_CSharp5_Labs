using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DemoSlide3.Models;

namespace DemoSlide3.Controllers;

/// <summary>
/// Controller xử lý authentication: Login và Logout
/// </summary>
public class AuthenticateController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<AuthenticateController> _logger;

    public AuthenticateController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        ILogger<AuthenticateController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    /// <summary>
    /// GET: /Authenticate/Login
    /// Hiển thị form đăng nhập
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        // Tạo ViewModel với ReturnUrl để redirect sau khi login
        var model = new LoginVm
        {
            ReturnUrl = returnUrl
        };
        return View(model);
    }

    /// <summary>
    /// POST: /Authenticate/Login
    /// Xử lý đăng nhập
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVm model)
    {
        // Kiểm tra validation
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Tìm user bằng email
        var user = await _userManager.FindByEmailAsync(model.Email);
        
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
            return View(model);
        }

        // Thực hiện đăng nhập bằng SignInManager
        // PasswordSignInAsync: kiểm tra password và tạo cookie authentication
        var result = await _signInManager.PasswordSignInAsync(
            user.UserName ?? model.Email, 
            model.Password, 
            isPersistent: false, // Không lưu cookie khi đóng trình duyệt
            lockoutOnFailure: false); // Không khóa tài khoản sau nhiều lần sai

        if (result.Succeeded)
        {
            _logger.LogInformation("User {Email} logged in successfully.", model.Email);
            
            // Redirect về ReturnUrl nếu có, ngược lại về Home/Index
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        // Đăng nhập thất bại
        ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
        return View(model);
    }

    /// <summary>
    /// POST: /Authenticate/Logout
    /// Đăng xuất người dùng
    /// </summary>
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        // Xóa cookie authentication
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out.");
        
        // Redirect về trang chủ
        return RedirectToAction("Index", "Home");
    }
}

