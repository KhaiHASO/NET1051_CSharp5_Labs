using Microsoft.AspNetCore.Identity;

namespace DemoBai123.Models;

/// <summary>
/// Custom Application User model kế thừa từ IdentityUser
/// Dùng chung cho tất cả các Bài (Bai1, Bai2, Bai3)
/// </summary>
public class ApplicationUser : IdentityUser
{
    // Có thể thêm các properties tùy chỉnh ở đây nếu cần
}

