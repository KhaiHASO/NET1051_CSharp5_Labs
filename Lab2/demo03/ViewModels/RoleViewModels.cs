using System.ComponentModel.DataAnnotations;

namespace demo03.ViewModels;

public class CreateRoleViewModel
{
    [Required]
    [Display(Name = "Role Name")]
    public string RoleName { get; set; }
}

public class EditRoleViewModel
{
    public EditRoleViewModel()
    {
        Users = new List<string>();
    }

    public string Id { get; set; }

    [Required(ErrorMessage = "Role Name is required")]
    public string RoleName { get; set; }

    public List<string> Users { get; set; }
}
