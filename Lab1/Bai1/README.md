# Lab 1: ASP.NET Core Identity (Bài 1)

Bài 1: Cài đặt và cấu hình ASP.NET Core Identity cơ bản.

---

## 1. Mục tiêu
Tạo dự án MVC, cài đặt thư viện Identity, cấu hình Database và chạy Migration.

## 2. Các bước thực hiện

### Bước 1: Tạo Project
```bash
dotnet new mvc -n Bai1
```

### Bước 2: Cài đặt Packages
Cần cài đặt các gói NuGet sau:
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `Microsoft.AspNetCore.Identity.UI`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`
- `Microsoft.VisualStudio.Web.CodeGeneration.Design`

### Bước 3: Tạo ApplicationDbContext
Tạo file `Data/ApplicationDbContext.cs` kế thừa từ `IdentityDbContext`.

```csharp
public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
```

### Bước 4: Cấu hình
**Program.cs**:
- Đăng ký `ApplicationDbContext`.
- Đăng ký Identity services: `AddDefaultIdentity`.
- Kích hoạt Middleware: `UseAuthentication` & `UseAuthorization`.

**appsettings.json**:
- Thêm chuỗi kết nối (Connection String) tới SQL Server (hoặc LocalDB).

### Bước 5: Migration
Chạy lệnh để sinh database:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 3. Kết quả
*   Custom Database `Bai1Db` được tạo ra trong SQL Server.
*   Các bảng Identity (`AspNetUsers`, `AspNetRoles`...) đã xuất hiện.

---
*Lab 1 - C# 5 - ASP.NET Core Identity*
