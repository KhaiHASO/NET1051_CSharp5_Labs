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

// app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await InitialDbSeed(services);
}

app.Run();

async Task InitialDbSeed(IServiceProvider services)
{
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    
    // 1. Tạo Admin User
    var adminEmail = "admin@neon.system";
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        await userManager.CreateAsync(admin, "Admin@123");
        // Gán claim Admin cho Bài 2
        await userManager.AddClaimAsync(admin, new Claim("Admin", "true"));
    }

    // 2. Tạo Sales User
    var salesEmail = "sales@neon.system";
    if (await userManager.FindByEmailAsync(salesEmail) == null)
    {
        var sales = new IdentityUser { UserName = salesEmail, Email = salesEmail, EmailConfirmed = true };
        await userManager.CreateAsync(sales, "Sales@123");
        // Gán claim Sales cho Bài 2 và CreateProduct cho Bài 1
        await userManager.AddClaimAsync(sales, new Claim("Sales", "true"));
        await userManager.AddClaimAsync(sales, new Claim("CreateProduct", "true"));
    }

    // 3. Tạo User thường (không có quyền)
    var userEmail = "dev@neon.system";
    if (await userManager.FindByEmailAsync(userEmail) == null)
    {
        var user = new IdentityUser { UserName = userEmail, Email = userEmail, EmailConfirmed = true };
        await userManager.CreateAsync(user, "User@123");
    }
}
