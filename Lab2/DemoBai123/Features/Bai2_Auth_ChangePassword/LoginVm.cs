using System.ComponentModel.DataAnnotations;

namespace DemoBai123.Features.Bai2_Auth_ChangePassword;

/// <summary>
/// ViewModel cho đăng nhập - Bài 2
/// </summary>
public class LoginVm
{
    [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
    [Display(Name = "Tên đăng nhập")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = string.Empty;
}

