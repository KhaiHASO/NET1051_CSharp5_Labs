using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DemoSlide3.Data;

var builder = WebApplication.CreateBuilder(args);

// Lấy connection string từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Cấu hình Entity Framework với SQL Server LocalDB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Cấu hình ASP.NET Core Identity
// Sử dụng IdentityUser mặc định (có Email, Password, v.v.)
// AddIdentity thay vì AddDefaultIdentity vì chúng ta dùng MVC, không dùng Razor Pages
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => 
{
    // Cấu hình password policy (tùy chọn, có thể điều chỉnh)
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
    
    // Cấu hình user
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Cấu hình cookie authentication để redirect về trang login tùy chỉnh
builder.Services.ConfigureApplicationCookie(options =>
{
    // Đường dẫn đến trang login tùy chỉnh
    options.LoginPath = "/Authenticate/Login";
    // Đường dẫn khi access bị denied
    options.AccessDeniedPath = "/Home/AccessDenied";
});

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

// Sử dụng Authentication và Authorization middleware
// Thứ tự quan trọng: UseAuthentication() phải đứng trước UseAuthorization()
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Seed user mẫu để test (chỉ chạy trong Development)
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        
        // Tạo user test nếu chưa có
        var testEmail = "test@example.com";
        if (await userManager.FindByEmailAsync(testEmail) == null)
        {
            var user = new IdentityUser 
            { 
                UserName = testEmail, 
                Email = testEmail,
                EmailConfirmed = true // Không cần confirm email trong demo
            };
            var result = await userManager.CreateAsync(user, "123"); // Password: 123
            
            if (result.Succeeded)
            {
                logger.LogInformation("Đã tạo user test: {Email}", testEmail);
            }
            else
            {
                logger.LogError("Không thể tạo user test: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}

app.Run();
