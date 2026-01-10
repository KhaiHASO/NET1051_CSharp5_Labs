using System.ComponentModel.DataAnnotations;

namespace demo02.ViewModels;

public class CreateRoleViewModel
{
    [Required]
    [Display(Name = "Role Name")]
    public string RoleName { get; set; }
}
