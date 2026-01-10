using Microsoft.AspNetCore.Identity;

namespace demo01.Models;

public class ApplicationUser : IdentityUser
{
    // Thêm các trường mở rộng
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}