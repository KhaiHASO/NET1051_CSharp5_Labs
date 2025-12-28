using System.ComponentModel.DataAnnotations;

namespace DemoSlide3.Models;

/// <summary>
/// ViewModel cho form đăng nhập
/// </summary>
public class LoginVm
{
    [Required(ErrorMessage = "Email là bắt buộc")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// URL để redirect sau khi login thành công
    /// </summary>
    public string? ReturnUrl { get; set; }
}

