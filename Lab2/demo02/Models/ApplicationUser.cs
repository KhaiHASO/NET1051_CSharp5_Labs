using Microsoft.AspNetCore.Identity;

namespace demo02.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
}
