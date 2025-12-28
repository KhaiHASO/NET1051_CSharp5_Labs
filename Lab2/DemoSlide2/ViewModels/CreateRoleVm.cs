using System.ComponentModel.DataAnnotations;

namespace DemoSlide2.ViewModels;

/// <summary>
/// ViewModel cho tạo role mới
/// </summary>
public class CreateRoleVm
{
    [Required(ErrorMessage = "Tên role là bắt buộc")]
    [Display(Name = "Tên Role")]
    public string RoleName { get; set; } = string.Empty;
}

