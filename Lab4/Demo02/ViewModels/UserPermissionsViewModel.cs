using System.ComponentModel.DataAnnotations;

namespace Demo02.ViewModels;

public class UserPermissionsViewModel
{
    public string UserId { get; set; }
    public string UserEmail { get; set; }
    public List<string> Permissions { get; set; } = new List<string>();
}
