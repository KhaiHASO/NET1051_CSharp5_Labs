namespace DemoBai123.Features.Bai3_Admin_Roles;

/// <summary>
/// ViewModel cho quản lý roles của user - Bài 3
/// </summary>
public class UserRoleVm
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<RoleCheckboxVm> Roles { get; set; } = new List<RoleCheckboxVm>();
}

/// <summary>
/// ViewModel cho checkbox role - Bài 3
/// </summary>
public class RoleCheckboxVm
{
    public string RoleId { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public bool IsSelected { get; set; }
}

