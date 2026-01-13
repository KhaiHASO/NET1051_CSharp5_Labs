using System.ComponentModel.DataAnnotations;

namespace Demo02.Models.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập Email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string? ReturnUrl { get; set; }
}
