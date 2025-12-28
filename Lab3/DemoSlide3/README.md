# DemoSlide3 - Authentication Identity

Project demo cho môn học **NET1051 – Lập trình C# 5**, phục vụ **DemoSlide3 (Authentication Identity)**.

## 📋 Mô Tả

Project này demo cách sử dụng **ASP.NET Core Identity** để xây dựng hệ thống authentication (xác thực người dùng) trong ứng dụng ASP.NET Core MVC.

## 🛠️ Công Nghệ Sử Dụng

- **ASP.NET Core MVC** (.NET 10.0)
- **ASP.NET Core Identity** - Quản lý authentication và authorization
- **Entity Framework Core** - ORM để làm việc với database
- **SQL Server LocalDB** - Database local
- **Bootstrap 5** - UI framework

## 📁 Cấu Trúc Project

```
DemoSlide3/
├── Controllers/
│   ├── AuthenticateController.cs    # Controller xử lý Login/Logout
│   └── HomeController.cs            # Controller chính, có action Secured
├── Data/
│   └── ApplicationDbContext.cs      # DbContext cho Identity
├── Models/
│   └── LoginVm.cs                    # ViewModel cho form đăng nhập
├── Views/
│   ├── Authenticate/
│   │   └── Login.cshtml             # View form đăng nhập
│   ├── Home/
│   │   └── Secured.cshtml           # View trang bảo mật
│   └── Shared/
│       └── _Layout.cshtml           # Layout chính
├── Program.cs                        # Cấu hình ứng dụng
├── appsettings.json                  # Cấu hình connection string
└── DemoSlide3.csproj                 # Project file
```

## 🚀 Hướng Dẫn Cài Đặt và Chạy

### Bước 1: Kiểm Tra Yêu Cầu

Đảm bảo bạn đã cài đặt:
- **.NET SDK 10.0** hoặc mới hơn
- **SQL Server LocalDB** (thường đi kèm với Visual Studio)
- **Visual Studio 2022** hoặc **VS Code** (tùy chọn)

### Bước 2: Restore Packages

Mở terminal trong thư mục project và chạy:

```bash
dotnet restore
```

### Bước 3: Tạo Database và Migration

Tạo migration đầu tiên:

```bash
dotnet ef migrations add InitialCreate
```

Tạo database từ migration:

```bash
dotnet ef database update
```

**Lưu ý:** Nếu gặp lỗi về `dotnet ef`, cần cài đặt tool:

```bash
dotnet tool install --global dotnet-ef
```

### Bước 4: Tạo User Để Test

Sau khi chạy project, bạn cần tạo user để đăng nhập. Có 2 cách:

#### Cách 1: Sử dụng Package Manager Console trong Visual Studio

```csharp
// Trong Package Manager Console
Add-Migration InitialCreate
Update-Database
```

Sau đó tạo user thủ công hoặc sử dụng Seed Data.

#### Cách 2: Tạo User Programmatically

Thêm code vào `Program.cs` để tự động tạo user khi chạy lần đầu (chỉ dùng cho development):

```csharp
// Thêm vào cuối Program.cs, trước app.Run()
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    
    // Tạo user nếu chưa có
    if (await userManager.FindByEmailAsync("test@example.com") == null)
    {
        var user = new IdentityUser { UserName = "test@example.com", Email = "test@example.com" };
        var result = await userManager.CreateAsync(user, "123"); // Password: 123
    }
}
```

### Bước 5: Chạy Project

```bash
dotnet run
```

Hoặc nhấn **F5** trong Visual Studio.

Project sẽ chạy tại: `https://localhost:5001` hoặc `http://localhost:5000`

## 🧪 Hướng Dẫn Test và Demo

### Demo Flow 1: Truy Cập Trang Secured (Chưa Đăng Nhập)

1. Mở trình duyệt và truy cập: `https://localhost:5001/Home/Secured`
2. **Kết quả:** Bạn sẽ bị redirect về `/Authenticate/Login?returnUrl=%2FHome%2FSecured`
3. **Quan sát:** URL có chứa `returnUrl` - đây là URL mà hệ thống sẽ redirect về sau khi login thành công

### Demo Flow 2: Đăng Nhập

1. Trên trang Login, nhập:
   - **Email:** `test@example.com` (hoặc email bạn đã tạo)
   - **Password:** `123` (hoặc password bạn đã tạo)
2. Click **Đăng Nhập**
3. **Kết quả:** Sau khi login thành công, bạn sẽ được redirect về `/Home/Secured` (từ ReturnUrl)
4. **Quan sát:** Trang Secured hiển thị "Hello" và thông tin user đã đăng nhập

### Demo Flow 3: Đăng Xuất

1. Trên trang Secured, click nút **Đăng Xuất**
2. **Kết quả:** Bạn sẽ được redirect về `/Home/Index`
3. **Quan sát:** Navbar không còn hiển thị tên user và có link "Đăng Nhập"

### Demo Flow 4: Truy Cập Secured Sau Khi Đăng Xuất

1. Sau khi đăng xuất, click link **Secured** trong navbar
2. **Kết quả:** Bạn lại bị redirect về trang Login
3. **Quan sát:** Hệ thống bảo vệ trang Secured bằng `[Authorize]` attribute

## 📝 Giải Thích Code

### 1. Cấu Hình Identity trong Program.cs

```csharp
// Cấu hình Entity Framework với SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Cấu hình ASP.NET Core Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => 
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    // ... các cấu hình khác
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Cấu hình cookie authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Authenticate/Login";
});
```

**Giải thích:**
- `AddDefaultIdentity`: Thêm Identity với `IdentityUser` mặc định
- `AddEntityFrameworkStores`: Lưu trữ Identity data trong database qua EF Core
- `ConfigureApplicationCookie`: Cấu hình đường dẫn login tùy chỉnh

### 2. [Authorize] Attribute

```csharp
[HttpGet]
[Authorize]
public IActionResult Secured()
{
    return View("Secured", "Hello");
}
```

**Giải thích:**
- `[Authorize]`: Yêu cầu user phải đăng nhập mới truy cập được action này
- Nếu chưa đăng nhập, ASP.NET Core tự động redirect về `/Authenticate/Login` (đã cấu hình trong Program.cs)
- ReturnUrl được tự động thêm vào query string

### 3. Login Logic trong AuthenticateController

```csharp
// Tìm user bằng email
var user = await _userManager.FindByEmailAsync(model.Email);

// Đăng nhập bằng SignInManager
var result = await _signInManager.PasswordSignInAsync(
    user.UserName ?? model.Email, 
    model.Password, 
    isPersistent: false,
    lockoutOnFailure: false);

if (result.Succeeded)
{
    // Redirect về ReturnUrl nếu có
    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
    {
        return Redirect(model.ReturnUrl);
    }
    return RedirectToAction("Index", "Home");
}
```

**Giải thích:**
- `UserManager`: Quản lý user (tìm, tạo, xóa user)
- `SignInManager`: Quản lý đăng nhập/đăng xuất (tạo cookie authentication)
- `PasswordSignInAsync`: Kiểm tra password và tạo authentication cookie
- `Url.IsLocalUrl`: Kiểm tra ReturnUrl có phải là URL local (bảo mật)

### 4. Logout Logic

```csharp
[HttpPost]
[Authorize]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Logout()
{
    await _signInManager.SignOutAsync();
    return RedirectToAction("Index", "Home");
}
```

**Giải thích:**
- `SignOutAsync`: Xóa authentication cookie
- Sau khi logout, user không còn authenticated nữa

## 🔍 Các Route Quan Trọng

| Route | Method | Mô Tả |
|-------|--------|-------|
| `/Home/Index` | GET | Trang chủ (không cần đăng nhập) |
| `/Home/Secured` | GET | Trang bảo mật (cần đăng nhập) |
| `/Authenticate/Login` | GET | Hiển thị form đăng nhập |
| `/Authenticate/Login` | POST | Xử lý đăng nhập |
| `/Authenticate/Logout` | POST | Đăng xuất |

## 📚 Các Khái Niệm Quan Trọng

### Authentication vs Authorization

- **Authentication (Xác thực):** Xác định "Bạn là ai?" - Kiểm tra user có đúng là người đó không (qua email/password)
- **Authorization (Phân quyền):** Xác định "Bạn được phép làm gì?" - Kiểm tra user có quyền truy cập resource không

### [Authorize] vs [AllowAnonymous]

- `[Authorize]`: Yêu cầu đăng nhập
- `[AllowAnonymous]`: Cho phép truy cập không cần đăng nhập (override [Authorize] ở controller level)

### UserManager vs SignInManager

- **UserManager**: Quản lý user (CRUD operations)
- **SignInManager**: Quản lý session đăng nhập (tạo/xóa cookie)

## 🐛 Troubleshooting

### Lỗi: "Connection string not found"

**Giải pháp:** Kiểm tra `appsettings.json` có connection string `DefaultConnection` chưa.

### Lỗi: "Cannot open database"

**Giải pháp:** 
1. Kiểm tra SQL Server LocalDB đã được cài đặt
2. Chạy lại `dotnet ef database update`
3. Kiểm tra connection string trong `appsettings.json`

### Lỗi: "dotnet ef command not found"

**Giải pháp:**
```bash
dotnet tool install --global dotnet-ef
```

### Không tìm thấy user để đăng nhập

**Giải pháp:** Tạo user trước khi test. Xem phần "Tạo User Để Test" ở trên.

## 📖 Tài Liệu Tham Khảo

- [ASP.NET Core Identity Documentation](https://learn.microsoft.com/aspnet/core/security/authentication/identity)
- [Entity Framework Core Documentation](https://learn.microsoft.com/ef/core/)
- [ASP.NET Core MVC Documentation](https://learn.microsoft.com/aspnet/core/mvc/)

## 👨‍💻 Tác Giả

Project demo cho môn học NET1051 – Lập trình C# 5

## 📄 License

Dùng cho mục đích giáo dục và học tập.

---

**Lưu ý:** Project này được thiết kế cho mục đích demo và học tập. Trong production, cần:
- Tăng cường bảo mật password (yêu cầu độ phức tạp cao hơn)
- Thêm email confirmation
- Thêm 2FA (Two-Factor Authentication)
- Xử lý lockout sau nhiều lần đăng nhập sai
- Sử dụng HTTPS trong production

