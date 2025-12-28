using System.ComponentModel.DataAnnotations;

namespace DemoBai123.Features.Bai3_Admin_Roles;

/// <summary>
/// ViewModel cho tạo role mới - Bài 3
/// </summary>
public class CreateRoleVm
{
    [Required(ErrorMessage = "Tên role là bắt buộc")]
    [Display(Name = "Tên Role")]
    public string RoleName { get; set; } = string.Empty;
}

