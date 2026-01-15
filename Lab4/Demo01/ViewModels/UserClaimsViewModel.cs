using System.ComponentModel.DataAnnotations;

namespace Demo01.ViewModels;

public class UserClaimsViewModel
{
    public string UserId { get; set; }
    public string UserEmail { get; set; }
    public List<ClaimViewModel> Claims { get; set; } = new List<ClaimViewModel>();
}

public class ClaimViewModel
{
    public string Type { get; set; }
    public string Value { get; set; }
}
