using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DemoSlide4.Data;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options =>
{
    // Bài 1: Tạo Policy CreateProductPolicy yêu cầu Claim CreateProduct
    options.AddPolicy("CreateProductPolicy", policy => 
        policy.RequireClaim("CreateProduct"));

    // Bài 2: Tạo Policy AdminViewProductPolicy yêu cầu Claim Admin
    options.AddPolicy("AdminViewProductPolicy", policy => 
        policy.RequireClaim("Admin"));

    // Bài 2: Tạo Policy SalesViewProductPolicy yêu cầu Claim Sales
    // và kiểm tra logic RequireAssertion: ID người dùng hiện tại phải trùng với CreatedBy của sản phẩm
    // Lưu ý: RequireAssertion này thường dùng cho logic kiểm tra dựa trên Resource (Product) 
    // Trong View hoặc Details, chúng ta sẽ kiểm tra thủ công hoặc dùng AuthorizationService
    options.AddPolicy("SalesViewProductPolicy", policy =>
    {
        policy.RequireClaim("Sales");
        policy.RequireAssertion(context =>
        {
            // Logic này kiểm tra claim Sales
            // Việc kiểm tra CreatedBy thường được thực hiện ở Resource-based Authorization
            // Nhưng theo yêu cầu Lab, ta có thể phác thảo logic kiểm tra ở đây nếu có Resource
            return context.User.HasClaim(c => c.Type == "Sales");
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages(); // Cần thiết cho Identity UI mặc định

app.Run();
