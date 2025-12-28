using System.ComponentModel.DataAnnotations;

namespace DemoBai123.Features.Bai3_Admin_Roles;

/// <summary>
/// ViewModel cho chỉnh sửa role - Bài 3
/// </summary>
public class EditRoleVm
{
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tên role là bắt buộc")]
    [Display(Name = "Tên Role")]
    public string RoleName { get; set; } = string.Empty;

    public List<string> Users { get; set; } = new List<string>();
}

