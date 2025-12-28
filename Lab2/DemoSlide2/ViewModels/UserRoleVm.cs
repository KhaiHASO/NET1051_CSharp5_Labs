namespace DemoSlide2.ViewModels;

/// <summary>
/// ViewModel cho quản lý roles của user
/// </summary>
public class UserRoleVm
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<RoleCheckboxVm> Roles { get; set; } = new List<RoleCheckboxVm>();
}

/// <summary>
/// ViewModel cho checkbox role
/// </summary>
public class RoleCheckboxVm
{
    public string RoleId { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public bool IsSelected { get; set; }
}

