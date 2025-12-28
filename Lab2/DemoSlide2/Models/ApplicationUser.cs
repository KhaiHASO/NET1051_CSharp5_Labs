using Microsoft.AspNetCore.Identity;

namespace DemoSlide2.Models;

/// <summary>
/// Custom Application User model kế thừa từ IdentityUser
/// </summary>
public class ApplicationUser : IdentityUser
{
    // Có thể thêm các properties tùy chỉnh ở đây nếu cần
    // Ví dụ: FullName, DateOfBirth, etc.
}

