using Microsoft.AspNetCore.Identity;

namespace bai4.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
