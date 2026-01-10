using System.ComponentModel.DataAnnotations;

namespace demo03.ViewModels;

public class UserViewModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public IEnumerable<string> Roles { get; set; }
}

public class EditUserViewModel
{
    public EditUserViewModel()
    {
        Roles = new List<RoleSelectionViewModel>();
    }

    public string Id { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required][EmailAddress]
    public string Email { get; set; }
    
    public string? FullName { get; set; }
    public string? Address { get; set; }

    public List<RoleSelectionViewModel> Roles { get; set; }
}

public class RoleSelectionViewModel
{
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public bool IsSelected { get; set; }
}
