using Microsoft.AspNetCore.Identity;

namespace bai3.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
