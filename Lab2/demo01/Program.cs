using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using demo01.Data;
using demo01.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. Đăng ký DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Đăng ký Identity với ApplicationUser (Thay vì IdentityUser mặc định)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    // Cấu hình Password (theo Slide 12)
    options.Password.RequireDigit = true; 
    options.Password.RequiredLength = 8; 
    options.Password.RequireUppercase = true; 
    options.Password.RequireLowercase = true;
    
    // Cấu hình Lockout (Khóa tài khoản)
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();