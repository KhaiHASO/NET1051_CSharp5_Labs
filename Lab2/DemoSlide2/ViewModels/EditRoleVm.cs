using System.ComponentModel.DataAnnotations;

namespace DemoSlide2.ViewModels;

/// <summary>
/// ViewModel cho chỉnh sửa role
/// </summary>
public class EditRoleVm
{
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tên role là bắt buộc")]
    [Display(Name = "Tên Role")]
    public string RoleName { get; set; } = string.Empty;

    public List<string> Users { get; set; } = new List<string>();
}

