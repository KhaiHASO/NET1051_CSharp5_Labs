using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DemoBai123.Models;

namespace DemoBai123.Data;

/// <summary>
/// DbContext cho Identity với ApplicationUser
/// Dùng chung cho tất cả các Bài (Bai1, Bai2, Bai3)
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Có thể thêm cấu hình tùy chỉnh cho entities ở đây
    }
}

