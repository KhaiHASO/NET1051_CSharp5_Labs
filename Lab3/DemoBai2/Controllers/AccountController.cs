using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DemoBai2.Models;
using Microsoft.AspNetCore.Authorization;

namespace DemoBai2.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            LoginVm model = new LoginVm
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm model)
        {
            if (ModelState.IsValid)
            {
                // Thực hiện đăng nhập
                // lockoutOnFailure: true để khóa tài khoản nếu đăng nhập sai nhiều lần
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Redirect về URL ban đầu hoặc trang chủ
                    return LocalRedirect(model.ReturnUrl ?? "/");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Đăng nhập thất bại. Vui lòng kiểm tra lại Email và Mật khẩu.");
                }
            }

            // Nếu thất bại, hiển thị lại form
            return View(model);
        }

        [HttpGet]
        [Authorize] // Yêu cầu đăng nhập mới được đổi mật khẩu
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVm model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                // Thực hiện đổi mật khẩu
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    // Đăng nhập lại để cập nhật Cookie security stamp (nếu cần thiết)
                    // await _signInManager.RefreshSignInAsync(user);

                    ViewBag.Message = "Đổi mật khẩu thành công!";
                    return View(); // Hoặc redirect về trang thông báo
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // Action logout để test việc Login lại
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
