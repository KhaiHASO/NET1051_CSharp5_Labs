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

public class CreateUserViewModel
{
    public CreateUserViewModel()
    {
        Roles = new List<RoleSelectionViewModel>();
    }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    [Display(Name = "User Name")]
    public string UserName { get; set; }

    [Display(Name = "Full Name")]
    public string FullName { get; set; }

    [Display(Name = "Address")]
    public string Address { get; set; }

    public List<RoleSelectionViewModel> Roles { get; set; }
}
